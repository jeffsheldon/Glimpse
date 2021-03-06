﻿using System;
using System.Reflection;
using System.Web;
using Castle.DynamicProxy;
using Glimpse.Net.Extensions;
using Glimpse.Net.Warning;

namespace Glimpse.Net.Interceptor
{
    //TODO: Create base class to refactor out NonVirtualMemberNotification. Create generic hook that just takes in a string array of method names?
    internal class ModelBinderProxyGenerationHook : IProxyGenerationHook
    {
        public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo)
        {
            var methodName = methodInfo.Name;

            var result = (methodName.Equals("BindModel") ||
                    methodName.Equals("BindProperty") ||
                    methodName.Equals("CreateModel"));

            return result;
        }

        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
        {
            var warnings = HttpContext.Current.GetWarnings();
            warnings.Add(new NonProxyableMemberWarning(type, memberInfo));
        }

        public void NonVirtualMemberNotification(Type type, MemberInfo memberInfo)
        {
            var warnings = HttpContext.Current.GetWarnings();
            warnings.Add(new NonVirtualMemberWarning(type, memberInfo));
        }

        public void MethodsInspected()
        {
        }
    }
}
