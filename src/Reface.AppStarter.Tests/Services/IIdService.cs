using Reface.AppStarter.Attributes;
using System;

namespace Reface.AppStarter.Tests.Services
{

    public interface IIdService
    {
        Guid Id { get; }
    }

    [Component]
    public class IdService : IIdService
    {
        private readonly Guid id = Guid.NewGuid();

        public Guid Id => this.id;
    }

    public class EmptyIdService : IIdService
    {
        public Guid Id => Guid.Empty;
    }
}
