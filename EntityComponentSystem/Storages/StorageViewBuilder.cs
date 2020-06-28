using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace EntityComponentSystem.Storages
{
  internal class StorageViewBuilder
  {
    private const string GetEntryMethodName = "GetEntry";

    public static Func<IStorageManager, IEnumerable<T>> CreateNewStorageView<T>()
      where T : ITuple
    {
      Type tupleType = typeof(T);
      Type[] neededTypes = tupleType.GetGenericArguments();

      return ConstructStorageView<T>(neededTypes);
    }

    private static Func<IStorageManager, IEnumerable<T>> ConstructStorageView<T>(Type[] neededTypes)
    {
      ParameterExpression result = Expression.Parameter(typeof(List<T>), "result");
      ParameterExpression i = Expression.Parameter(typeof(int), "i");
      ParameterExpression dataLength = Expression.Parameter(typeof(int), "dataLength");
      ParameterExpression storageManager = Expression.Parameter(typeof(IStorageManager), "storageManager");
      Expression newListExpression = Expression.New(typeof(List<T>));
      LabelTarget endLabel = Expression.Label();
      LabelTarget nextIndexLabel = Expression.Label();

      MethodInfo dataLengthMethod = typeof(IStorageManager).GetProperty(nameof(IStorageManager.DataLength)).GetMethod;

      BlockExpression block = Expression.Block(
        typeof(List<T>),
        new[] { result, dataLength, i },
        Expression.Assign(dataLength, Expression.Call(storageManager, dataLengthMethod)),
        Expression.Assign(result, newListExpression),
        Expression.Assign(i, Expression.Constant(-1)),

        Expression.Loop(
          Expression.Block(
            Expression.AddAssign(i, Expression.Constant(1)),
            Expression.IfThenElse(
              Expression.LessThan(i, dataLength),
              CreateCheckIndexExpression<T>(i, neededTypes, storageManager, nextIndexLabel, result),
              Expression.Break(endLabel, result)
            )
          ),
          endLabel,
          nextIndexLabel
        ),
        result
      );

      var lambda = Expression.Lambda<Func<IStorageManager, IEnumerable<T>>>(block, storageManager);
      return lambda.Compile();
    }

    private static Expression CreateCheckIndexExpression<T>(ParameterExpression i,
                                                            Type[] neededTypes,
                                                            ParameterExpression storageManager,
                                                            LabelTarget nextIndexLabel,
                                                            ParameterExpression resultList)
    {
      List<Expression> expressions = new List<Expression>();
      List<ParameterExpression> parameters = new List<ParameterExpression>();
      ParameterExpression tupleResult = Expression.Parameter(typeof(T));

      string getStorageMethodName = nameof(IStorageManager.GetStorage);

      foreach (Type t in neededTypes)
      {
        ParameterExpression parameter = Expression.Parameter(t);

        parameters.Add(parameter);
        expressions.Add(Expression.Assign(parameter, GetEntryExpression(i, storageManager, getStorageMethodName, t)));
        expressions.Add(Expression.IfThen(
                          Expression.Equal(parameter, Expression.Constant(null, t)),
                          Expression.Break(nextIndexLabel)
                        ));
      }

      expressions.Add(Expression.Assign(tupleResult, Expression.New(typeof(T).GetConstructor(neededTypes), parameters.ToArray())));
      expressions.Add(Expression.Call(resultList, nameof(IList<T>.Add), Array.Empty<Type>(), tupleResult));

      parameters.Add(tupleResult);

      return Expression.Block(
               parameters.ToArray(),
               expressions.ToArray()
             );
    }

    private static Expression GetEntryExpression(ParameterExpression indexParameter,
                                                 ParameterExpression storageManagerParameter,
                                                 string getStorageMethodName,
                                                 Type type)
    {
      Expression valueExpression = Expression.Call(storageManagerParameter, getStorageMethodName, new Type[] { type });
      return Expression.Call(valueExpression, GetEntryMethodName, null, indexParameter);
    }

  }
}
