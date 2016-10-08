using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace WeakDelegateProject
{
    public class DelegateFactory
    {
        private MethodInfo targetMethodInfo;
        private WeakReference weakReference;

        public DelegateFactory(WeakReference weakReference, MethodInfo targetMethodInfo)
        {
            this.weakReference = weakReference;
            this.targetMethodInfo = targetMethodInfo;
        }

        public Delegate GetDelegate()
        {
            var argumentsType = GetArgumentsType();
            return Expression.Lambda(GetDelegateType(), GetBlockCall(argumentsType, CallAction(argumentsType)), argumentsType).Compile();
        }

        private Type GetDelegateType()
        {
            return Expression.GetDelegateType(targetMethodInfo.GetParameters().Select(parameter => parameter.ParameterType).Concat(new[] { targetMethodInfo.ReturnType }).ToArray());
        }

        private ConditionalExpression GetBlockCall(ParameterExpression[] argumentsType, Expression[] callbackTarget)
        {
            return Expression.IfThen(Expression.IsTrue(GetCheckIsALive()), Expression.Block(GetVariables(argumentsType), callbackTarget));
        }

        private Expression[] CallAction(ParameterExpression[] argumentsType)
        {
            return new Expression[] { CallDelegate(argumentsType) };
        }

        private Expression[] CallFunc(ParameterExpression[] argumentsType, ParameterExpression returnVariable)
        {
            return new Expression[] { Expression.Assign(returnVariable, CallDelegate(argumentsType)) };
        }

        private MethodCallExpression CallDelegate(ParameterExpression[] argumentsType)
        {
            return Expression.Call(instance: Expression.Convert(GetTarget(),
                targetMethodInfo.DeclaringType), method: targetMethodInfo, arguments: argumentsType);
        }

        private List<ParameterExpression> GetVariables(ParameterExpression[] argumentsType)
        {
            return new List<ParameterExpression>(argumentsType.Select(parameter => Expression.Variable(parameter.Type)));
        }

        private MemberExpression GetTarget()
        {
            return Expression.Property(Expression.Convert(Expression.Constant(weakReference), typeof(WeakReference)), "Target");
        }

        private MemberExpression GetCheckIsALive()
        {
            return Expression.Property(Expression.Convert(Expression.Constant(weakReference), typeof(WeakReference)), "IsAlive");
        }

        private ParameterExpression[] GetArgumentsType()
        {
            return targetMethodInfo.GetParameters().Select(parameter => Expression.Parameter(parameter.ParameterType)).ToArray();
        }
    }









    /*
    public class DelegateFactory
    {
        private MethodInfo targetMethodInfo;
        private WeakReference weakReference;

        public DelegateFactory(WeakReference weakReference, MethodInfo targetMethodInfo)
        {
            this.weakReference = weakReference;
            this.targetMethodInfo = targetMethodInfo;
        }

        public Delegate GetDelegate()
        {
            ParameterExpression[] eventHandlerParametersExpression = GenerateParametersExpression(targetMethodInfo);

            Expression weakReferenceExpression = Expression.Constant(weakReference);
            Type eventHandlerDelegateType = weakReference.Target.GetType();
            Expression targetObjectExpression = GetPropertyExpression(weakReferenceExpression, "Target", eventHandlerDelegateType);

            Expression targetMethodInvoke = Expression.Call(targetObjectExpression, targetMethodInfo, eventHandlerParametersExpression);

            Expression nullExpression = Expression.Constant(null);
            Expression conditionExpression = Expression.NotEqual(targetObjectExpression, nullExpression);
            Expression ifExpression = Expression.IfThen(conditionExpression, targetMethodInvoke);

            LambdaExpression labmda = Expression.Lambda(ifExpression, eventHandlerParametersExpression);
            return labmda.Compile();
        }

        private ParameterExpression[] GenerateParametersExpression(MethodInfo method)
        {
            ParameterInfo[] eventHandlerParametersInfo = method.GetParameters();
            ParameterExpression[] eventHandlerParametersExpression =
                new ParameterExpression[eventHandlerParametersInfo.Length];
            int i = 0;
            foreach (ParameterInfo parameter in eventHandlerParametersInfo)
            {
                eventHandlerParametersExpression[i] = Expression.Parameter(parameter.ParameterType);
                i++;
            }
            return eventHandlerParametersExpression;
        }

        private Expression GetPropertyExpression(Expression objectExpression, String propertyName,
            Type typeToCastProperty = null)
        {
            Type classType = objectExpression.Type;
            PropertyInfo targetPropertyInfo = classType.GetProperty(propertyName);
            Expression targetObjectExpression = Expression.Property(objectExpression, targetPropertyInfo);
            if (typeToCastProperty != null)
            {
                targetObjectExpression = Expression.Convert(targetObjectExpression, typeToCastProperty);
            }
            return targetObjectExpression;
        }

    }
    */
}