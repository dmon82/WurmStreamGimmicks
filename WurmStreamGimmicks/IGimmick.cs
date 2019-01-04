using System;
using System.Collections.Generic;

namespace WurmStreamGimmicks {
    /// <summary>
    ///     The general gimmick interface class.
    /// </summary>
    internal interface IGimmick {
        /// <summary>
        ///     Gets the name of the gimmick.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Gets whether or not log messages from all clients are monitored or just the specific ones. Overrides <see cref="IGimmick.Players"/> list.
        /// </summary>
        bool Collective { get; }

        /// <summary>
        ///     Gets the list of players that logs are monitored. If <see cref="IGimmick.Collective"/> is set to true, the list is ignored.
        /// </summary>
        List<Player> Players { get; }

        /// <summary>
        ///     Watch a line from event, combat, or skills log.
        /// </summary>
        /// <param name="line">The line from the logfile.</param>
        void Watch(string line);

        /// <summary>
        ///     Compiles the text output of the gimmick as per configuration.
        /// </summary>
        /// <returns>Gimmick text output.</returns>
        string Compile();
    }
}
