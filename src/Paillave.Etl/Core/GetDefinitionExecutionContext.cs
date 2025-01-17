﻿using Paillave.Etl.Core;
using Paillave.Etl.Reactive.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Paillave.Etl.Core
{
    public class GetDefinitionExecutionContext : IExecutionContext
    {
        private List<StreamToNodeLink> _streamToNodeLinks = new List<StreamToNodeLink>();
        private List<INodeDescription> _nodes = new List<INodeDescription>();
        public JobDefinitionStructure GetDefinitionStructure() => new JobDefinitionStructure(_streamToNodeLinks, _nodes, this.JobName);
        // private class RootNode : INodeDescription
        // {
        //     public string NodeName => "-ProcessRunner-";
        //     public string TypeName => "-ProcessRunner-";
        //     public ProcessImpact PerformanceImpact => ProcessImpact.Light;
        //     public ProcessImpact MemoryFootPrint => ProcessImpact.Light;
        //     public INodeDescription Parent => null;
        // }
        public GetDefinitionExecutionContext(string jobName, IFileValueConnectors connectors)
        {
            this.JobName = jobName;
            this.Connectors = connectors;
            // _nodes.Add(new RootNode());
        }
        public void AddStreamToNodeLink(StreamToNodeLink link) => _streamToNodeLinks.Add(link);
        public Guid ExecutionId => Guid.Empty;
        public WaitHandle StartSynchronizer => throw new NotImplementedException();
        public string JobName { get; }
        public bool IsTracingContext => false;
        public void AddDisposable(IDisposable disposable) { }
        public void AddUnderlyingDisposables(StreamWithResource disposable) { }
        public void AddNode<T>(INodeDescription nodeContext, IPushObservable<T> observable) => _nodes.Add(nodeContext);
        public SimpleDependencyResolver ContextBag => new SimpleDependencyResolver();
        public IFileValueConnectors Connectors { get; }
        public IDependencyResolver DependencyResolver => new DummyDependencyResolver();

        public bool Terminating => throw new NotImplementedException();

        public bool UseDetailedTraces => false;

        public Task GetCompletionTask() => throw new NotImplementedException();
        public int NextTraceSequence() => 0;
        public Task InvokeInDedicatedThreadAsync(object threadOwner, Action action) => throw new NotImplementedException();
        public Task<T> InvokeInDedicatedThreadAsync<T>(object threadOwner, Func<T> action) => throw new NotImplementedException();
        public object GetOrCreateFromContextBag(string key, Func<object> creator) => throw new NotImplementedException();
        public T GetOrCreateFromContextBag<T>(Func<T> creator) => throw new NotImplementedException();
        public void AddTrace(ITraceContent traceContent, INodeContext sourceNode) { }
    }
}
