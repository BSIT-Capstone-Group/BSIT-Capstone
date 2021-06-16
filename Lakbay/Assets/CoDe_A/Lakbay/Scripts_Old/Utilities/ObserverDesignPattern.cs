using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoDe_A_Old.Lakbay.Utilities.ObserverDesignPattern {
    public interface ISubscriber {
        public void updateWithSource(Source source);

    }

    public interface ISource {
        public void addSubscriber(ISubscriber subscriber);
        public void removeSubscriber(ISubscriber subscriber);
        public void removeAllSubscribers();
        public void updateSubscribers();

    }

    public abstract class Source : ISource {
        private List<ISubscriber> _subscribers;

        public List<ISubscriber> subscribers {
            get => this._subscribers;
        }

        public Source() {
            this._subscribers = new List<ISubscriber>();

        }

        public void addSubscriber(ISubscriber subscriber) {
            this._subscribers.Add(subscriber);

        }

        public void removeSubscriber(ISubscriber subscriber) {
            this._subscribers.Remove(subscriber);

        }

        public void removeAllSubscribers() {
            this._subscribers.Clear();

        }

        public void updateSubscribers() {
            foreach(ISubscriber sub in this._subscribers) {
                sub.updateWithSource(this);

            }

        }

    }

    public abstract class _SourceAndSubscriber : Source, ISubscriber {
        public void updateWithSource(Source source) {}

    }

    public abstract class SourceAndSubscriber : Utilities.ExtendedMonoBehaviour {
        

    }

}