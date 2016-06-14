﻿using System;

namespace DynamicTranslator.Domain.Uow
{
    #region using

    

    #endregion

    /// <summary>
    ///     Used as event arguments on <see cref="IActiveUnitOfWork.Failed" /> event.
    /// </summary>
    public class UnitOfWorkFailedEventArgs : EventArgs
    {
        /// <summary>
        ///     Creates a new <see cref="UnitOfWorkFailedEventArgs" /> object.
        /// </summary>
        /// <param name="exception">Exception that caused failure</param>
        public UnitOfWorkFailedEventArgs(System.Exception exception)
        {
            Exception = exception;
        }

        /// <summary>
        ///     Exception that caused failure.
        /// </summary>
        public System.Exception Exception { get; private set; }
    }
}