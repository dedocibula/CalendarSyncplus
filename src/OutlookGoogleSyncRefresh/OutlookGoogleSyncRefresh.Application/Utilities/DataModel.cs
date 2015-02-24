﻿#region File Header
// /******************************************************************************
//  * 
//  *      Copyright (C) Ankesh Dave 2015 All Rights Reserved. Confidential
//  * 
//  ******************************************************************************
//  * 
//  *      Project:        OutlookGoogleSyncRefresh
//  *      SubProject:     OutlookGoogleSyncRefresh.Application
//  *      Author:         Dave, Ankesh
//  *      Created On:     06-02-2015 12:41 PM
//  *      Modified On:    06-02-2015 12:41 PM
//  *      FileName:       DataModel.cs
//  * 
//  *****************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Waf.Foundation;
using System.Waf.Applications;




using OutlookGoogleSyncRefresh.Domain.Models;

using Model = OutlookGoogleSyncRefresh.Domain.Models.Model;

namespace OutlookGoogleSyncRefresh.Application.Utilities
{
    /// <summary>
    /// Abstract base class for a DataModel implementation.
    /// 
    /// </summary>
    [Serializable]
    public abstract class DataModel : Model
    {
        [NonSerialized]
        private readonly List<PropertyChangedEventListener> propertyChangedListeners = new List<PropertyChangedEventListener>();
        [NonSerialized]
        private readonly List<CollectionChangedEventListener> collectionChangedListeners = new List<CollectionChangedEventListener>();

        /// <summary>
        /// Adds a weak event listener for a PropertyChanged event.
        /// 
        /// </summary>
        /// <param name="source">The source of the event.</param><param name="handler">The event handler.</param><exception cref="T:System.ArgumentNullException">source must not be <c>null</c>.</exception><exception cref="T:System.ArgumentNullException">handler must not be <c>null</c>.</exception>
        protected void AddWeakEventListener(INotifyPropertyChanged source, PropertyChangedEventHandler handler)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (handler == null)
                throw new ArgumentNullException("handler");
            PropertyChangedEventListener changedEventListener = new PropertyChangedEventListener(source, handler);
            this.propertyChangedListeners.Add(changedEventListener);
            PropertyChangedEventManager.AddListener(source, (IWeakEventListener)changedEventListener, "");
        }

        /// <summary>
        /// Removes the weak event listener for a PropertyChanged event.
        /// 
        /// </summary>
        /// <param name="source">The source of the event.</param><param name="handler">The event handler.</param><exception cref="T:System.ArgumentNullException">source must not be <c>null</c>.</exception><exception cref="T:System.ArgumentNullException">handler must not be <c>null</c>.</exception>
        protected void RemoveWeakEventListener(INotifyPropertyChanged source, PropertyChangedEventHandler handler)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (handler == null)
                throw new ArgumentNullException("handler");
            PropertyChangedEventListener changedEventListener = Enumerable.LastOrDefault<PropertyChangedEventListener>((IEnumerable<PropertyChangedEventListener>)this.propertyChangedListeners, (Func<PropertyChangedEventListener, bool>)(l =>
            {
                if (l.Source == source)
                    return l.Handler == handler;
                else
                    return false;
            }));
            if (changedEventListener == null)
                return;
            this.propertyChangedListeners.Remove(changedEventListener);
            PropertyChangedEventManager.RemoveListener(source, (IWeakEventListener)changedEventListener, "");
        }

        /// <summary>
        /// Adds a weak event listener for a CollectionChanged event.
        /// 
        /// </summary>
        /// <param name="source">The source of the event.</param><param name="handler">The event handler.</param><exception cref="T:System.ArgumentNullException">source must not be <c>null</c>.</exception><exception cref="T:System.ArgumentNullException">handler must not be <c>null</c>.</exception>
        protected void AddWeakEventListener(INotifyCollectionChanged source, NotifyCollectionChangedEventHandler handler)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (handler == null)
                throw new ArgumentNullException("handler");
            CollectionChangedEventListener changedEventListener = new CollectionChangedEventListener(source, handler);
            this.collectionChangedListeners.Add(changedEventListener);
            CollectionChangedEventManager.AddListener(source, (IWeakEventListener)changedEventListener);
        }

        /// <summary>
        /// Removes the weak event listener for a CollectionChanged event.
        /// 
        /// </summary>
        /// <param name="source">The source of the event.</param><param name="handler">The event handler.</param><exception cref="T:System.ArgumentNullException">source must not be <c>null</c>.</exception><exception cref="T:System.ArgumentNullException">handler must not be <c>null</c>.</exception>
        protected void RemoveWeakEventListener(INotifyCollectionChanged source, NotifyCollectionChangedEventHandler handler)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (handler == null)
                throw new ArgumentNullException("handler");
            CollectionChangedEventListener changedEventListener = Enumerable.LastOrDefault<CollectionChangedEventListener>((IEnumerable<CollectionChangedEventListener>)this.collectionChangedListeners, (Func<CollectionChangedEventListener, bool>)(l =>
            {
                if (l.Source == source)
                    return l.Handler == handler;
                else
                    return false;
            }));
            if (changedEventListener == null)
                return;
            this.collectionChangedListeners.Remove(changedEventListener);
            CollectionChangedEventManager.RemoveListener(source, (IWeakEventListener)changedEventListener);
        }
    }
}