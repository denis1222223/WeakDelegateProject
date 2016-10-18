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
            var parametersType = GetParametersType();
            return Expression.Lambda(GetDelegateType(), GetBlockCall(parametersType, CallAction(parametersType)), parametersType).Compile();
        }

        private Type GetDelegateType()
        {
            return Expression.GetDelegateType(targetMethodInfo.GetParameters().Select(parameter =>
                parameter.ParameterType).Concat(new[] { targetMethodInfo.ReturnType }).ToArray());
        }

        private ConditionalExpression GetBlockCall(ParameterExpression[] parametersType, Expression[] callbackTarget)
        {
            return Expression.IfThen(Expression.IsTrue(GetCheckIsALive()), Expression.Block(GetVariables(parametersType), callbackTarget));
        }

        private Expression[] CallAction(ParameterExpression[] parametersType)
        {
            return new Expression[] { CallDelegate(parametersType) };
        }

        private MethodCallExpression CallDelegate(ParameterExpression[] argumentsType)
        {
            return Expression.Call(
                instance: Expression.Convert(GetTarget(), targetMethodInfo.DeclaringType), 
                method: targetMethodInfo, 
                arguments: argumentsType);
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

        private ParameterExpression[] GetParametersType()
        {
            return targetMethodInfo.GetParameters().Select(parameter => Expression.Parameter(parameter.ParameterType)).ToArray();
        }
    }
}