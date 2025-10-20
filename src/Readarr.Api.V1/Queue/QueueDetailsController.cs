using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Readarr.Common.Extensions;
using Readarr.Core.Datastore.Events;
using Readarr.Core.Download.Pending;
using Readarr.Core.Messaging.Events;
using Readarr.Core.Queue;
using Readarr.Http;
using Readarr.Http.REST;
using Readarr.SignalR;

namespace Readarr.Api.V1.Queue
{
    [V5ApiController("queue/details")]
    public class QueueDetailsController : RestControllerWithSignalR<QueueResource, Readarr.Core.Queue.Queue>,
                               IHandle<QueueUpdatedEvent>, IHandle<PendingReleasesUpdatedEvent>
    {
        private readonly IQueueService _queueService;
        private readonly IPendingReleaseService _pendingReleaseService;

        public QueueDetailsController(IBroadcastSignalRMessage broadcastSignalRMessage, IQueueService queueService, IPendingReleaseService pendingReleaseService)
            : base(broadcastSignalRMessage)
        {
            _queueService = queueService;
            _pendingReleaseService = pendingReleaseService;
        }

        [NonAction]
        public override ActionResult<QueueResource> GetResourceByIdWithErrorHandler(int id)
        {
            return base.GetResourceByIdWithErrorHandler(id);
        }

        protected override QueueResource GetResourceById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Produces("application/json")]
        public List<QueueResource> GetQueue(int? seriesId, [FromQuery]List<int> episodeIds, bool includeSeries = false, bool includeEpisodes = false)
        {
            var queue = _queueService.GetQueue();
            var pending = _pendingReleaseService.GetPendingQueue();
            var fullQueue = queue.Concat(pending);

            if (seriesId.HasValue)
            {
                return fullQueue.Where(q => q.Series?.Id == seriesId).ToResource(includeSeries, includeEpisodes);
            }

            if (episodeIds.Any())
            {
                return fullQueue.Where(q => q.Episodes.Any() &&
                                            episodeIds.IntersectBy(e => e, q.Episodes, e => e.Id, null).Any())
                    .ToResource(includeSeries, includeEpisodes);
            }

            return fullQueue.ToResource(includeSeries, includeEpisodes);
        }

        [NonAction]
        public void Handle(QueueUpdatedEvent message)
        {
            BroadcastResourceChange(ModelAction.Sync);
        }

        [NonAction]
        public void Handle(PendingReleasesUpdatedEvent message)
        {
            BroadcastResourceChange(ModelAction.Sync);
        }
    }
}
