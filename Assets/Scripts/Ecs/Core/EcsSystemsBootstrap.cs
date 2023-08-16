using System;
using System.Collections.Generic;
using Entitas;
using Zenject;

namespace Scripts.Ecs.Core
{
	public class EcsSystemsBootstrap : ITickable, IInitializable, IDisposable
	{
		private readonly List<IContext> _contexts;
		private readonly Feature _systems;
		private bool _isPaused;

		public EcsSystemsBootstrap
		(
			string name,
			[InjectLocal] List<IContext> contexts,
			[InjectLocal] List<ISystem> systems
		)
		{
			_contexts = contexts;
			_systems = new Feature($"Bootstrap [{name}]");
			for (var i = 0; i < systems.Count; i++) 
				_systems.Add(systems[i]);
		}

		public void Initialize()
		{
			if (_isPaused)
				return;

			_systems.Initialize();
		}
        
		public void Dispose()
		{
			_systems.DeactivateReactiveSystems();
			_systems.ClearReactiveSystems();
			foreach (var context in _contexts)
				context.Reset();
		}
		
		public void Tick()
		{
			if (_isPaused)
				return;

			_systems.Execute();
		}
	}
}