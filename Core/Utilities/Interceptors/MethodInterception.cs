﻿using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception:MethodInterceptionBaseAttribute
    {

        protected virtual void OnBefore(IInvocation invocation) { } //Method çalışmadan önce sen çalış
        protected virtual void OnAfter(IInvocation invocation) { } //Method çalıştıktan sonra sen çalış
        protected virtual void OnException(IInvocation invocation,System.Exception e) { } //Method hata verdiğinde sen çalış
        protected virtual void OnSucces(IInvocation invocation) { } //Method başarılı ise sen çalış

        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation,e);
                throw;
            }
            finally
            {
                if(isSuccess)
                {
                    OnSucces(invocation);
                }
            }
            OnAfter(invocation);
        }
    }
}
