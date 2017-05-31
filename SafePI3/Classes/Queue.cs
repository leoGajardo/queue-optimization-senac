using SafePI3.Interfaces;
using SafePI3.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePI3.Classes
{
    public class Queue : IQueue, IDisposable
    {
        private ObservableCollection<Client> _Clients;
        public int TimePerClient { get; private set; }

        public List<Client> AllClients { get; set; }

        public ObservableCollection<Client> Clients
        {
           get
            {
                return _Clients;
            }
            set
            {
                if (value != _Clients) { 
                    _Clients = value;
                    UpdateQueueUserControl();
                }
            }
        }

        private void Clients_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateQueueUserControl();
        }
        
        private string _Name;
        public string Name
        {
           get
            {
                return _Name;
            }
            set
            {
                if (value != _Name) { 
                    _Name = value;
                    UpdateQueueUserControl();
                }
            }
        }

        public int OperatorsJustArrived { get; set; }

        private int _OperatorsQuantity;
        public int OperatorsQuantity
        {
           get
            {
                return _OperatorsQuantity;
            }
            set
            {
                if (value != _OperatorsQuantity) {
                    if (!(value > ServiceDesksQuantity))
                    {
                        _OperatorsQuantity = value;
                        UpdateQueueUserControl();
                    }
                    else
                    {
                        if (value > ServiceDesksQuantity)
                        {
                            throw new InvalidOperationException();
                        }
                    }
                }
            }
        }

        private int _QueuesQuantity;
        public int QueuesQuantity
        {
           get
            {
                return _QueuesQuantity;
            }
            set
            {
                if (value != _QueuesQuantity) { 
                    _QueuesQuantity = value;
                    UpdateQueueUserControl();
                }
            }
        }

        private int _ServiceDesksQuantity;
        public int ServiceDesksQuantity
        {
           get
            {
                return _ServiceDesksQuantity;
            }
            set
            {
                if (value != _ServiceDesksQuantity) { 
                    _ServiceDesksQuantity = value;
                    UpdateQueueUserControl();
                }
            }
        }

        private string _Label;
        public string Label
        {
            get
            {
                return _Label;
            }
            set
            {
                if (value != _Label) { 
                    _Label = value;
                    UpdateQueueUserControl();
                }
            }
        }

        private QueueUC _QueueUserControl;

        

        public QueueUC QueueUserControl
        {
            get
            {
                if (_QueueUserControl == null)
                {
                    _QueueUserControl = new QueueUC(this);
                }
                return _QueueUserControl;
            }
            set
            {
                _QueueUserControl = value;
            }
        }

        public Queue(string label , string name , int operatorsQuantity , int queuesQuantity , int serviceDesksQuantity , int timePerClient)
        {
            _Label = label;
            _Name = name;
            _OperatorsQuantity = operatorsQuantity;
            _QueuesQuantity = queuesQuantity;
            _ServiceDesksQuantity = serviceDesksQuantity;
            _Clients = new ObservableCollection<Client>();
            _Clients.CollectionChanged += Clients_CollectionChanged;
            TimePerClient = timePerClient;
            AllClients = new List<Client>();
        }

        

        public void Dispose()
        {
            QueueUserControl.Dispose();
        }

        private void UpdateQueueUserControl()
        {
            QueueUserControl.UpdateQueue();
        }
    }
}
