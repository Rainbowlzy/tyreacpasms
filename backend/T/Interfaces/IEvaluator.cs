using System;
using T.Models;

namespace T.Interfaces
{
    public interface IEvaluator:IDisposable
    {
        /// <summary>
        /// 求值函数
        /// </summary>
        /// <param name="request">数据</param>
        /// <returns>返回值</returns>
        object Eval(CommonRequest request);
    }
}