using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Analytics.Commands;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Analytics.Repositories;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Handlers;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Notifications;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Responses;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Messages;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Analytics.Handlers
{
    public class AnalyticsCommandHandler : CommandHandlerBase, ICommandHandler<TrackResponseCommand>
    {
        private readonly IMapper _mapper;
        private readonly IResponseTrackingRepository _repository;

        public AnalyticsCommandHandler(
            DomainNotificationContext notificationContext,
            CommonMessages commonMessages,
            IUnitOfWork uow,
            IMapper mapper,
            IResponseTrackingRepository responseTrackingRepository) : base(notificationContext, commonMessages, uow)
        {
            _repository = responseTrackingRepository;
            _mapper = mapper;
        }


        public async Task<IResponse> Handle(TrackResponseCommand request, CancellationToken cancellationToken)
        {
            var track = _mapper.Map<ResponseTracking>(request);

            await _repository.AddAsync(track);

            if (await Commit())
                return NoContent();
            return Fail();
        }
    }
}