﻿using System;
using System.Threading;

namespace GitHub.Unity
{
    class GitCommitTask : ProcessTask<string>
    {
        private const string TaskName = "git commit";
        private readonly string arguments;

        public GitCommitTask(string message, string body,
            CancellationToken token, IOutputProcessor<string> processor = null)
            : base(token, processor ?? new SimpleOutputProcessor())
        {
            Guard.ArgumentNotNullOrWhiteSpace(message, "message");

            Name = TaskName;
            arguments = "commit ";
            arguments += String.Format(" -m \"{0}", message);
            if (!String.IsNullOrEmpty(body))
                arguments += String.Format("{0}{1}", Environment.NewLine, body);
            arguments += "\"";
        }

        public override string ProcessArguments { get { return arguments; } }
        public override TaskAffinity Affinity { get { return TaskAffinity.Exclusive; } }
    }
}
