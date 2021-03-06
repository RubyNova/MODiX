﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Modix.Data.Models;
using Modix.Data.Repositories;
using Newtonsoft.Json;

namespace Modix.Services.BehaviourConfiguration
{
    public interface IBehaviourConfigurationService
    {
        Task LoadBehaviourConfiguration();
    }

    public class BehaviourConfigurationService : IBehaviourConfigurationService
    {
        private readonly IBehaviourConfigurationRepository _behaviourConfigurationRepository;
        private readonly IBehaviourConfiguration _behaviourConfiguration;

        public BehaviourConfigurationService(IBehaviourConfigurationRepository behaviourConfigurationRepository, IBehaviourConfiguration behaviourConfiguration)
        {
            _behaviourConfigurationRepository = behaviourConfigurationRepository;
            _behaviourConfiguration = behaviourConfiguration;
        }

        public async Task LoadBehaviourConfiguration()
        {
            var behaviours = await _behaviourConfigurationRepository.GetBehaviours();

            var invitePurgeBehaviour =
                BuildInvitePurgeBehaviour(behaviours.Where(x => x.Category == BehaviourCategory.InvitePurging).ToList());

            _behaviourConfiguration.InvitePurgeBehaviour = invitePurgeBehaviour;
        }

        private static InvitePurgeBehaviour BuildInvitePurgeBehaviour(List<Data.Models.BehaviourConfiguration> behaviours)
        {
            const string EnabledKey = "IsEnabled";
            const string ExemptRoleIds = "ExemptRoleIds";
            const string LoggingChannelId = "LoggingChannelId";

            return new InvitePurgeBehaviour
            {
                IsEnabled = bool.Parse(behaviours.Single(x => x.Key == EnabledKey).Value),
                ExemptRoleIds = JsonConvert.DeserializeObject<List<ulong>>(behaviours.Single(x => x.Key == ExemptRoleIds).Value),
                LoggingChannelId = ulong.Parse(behaviours.Single(x => x.Key == LoggingChannelId).Value)
            };
        }
    }
}
