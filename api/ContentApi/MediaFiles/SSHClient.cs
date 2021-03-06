﻿using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ContentApi.MediaFiles
{
    public class SSHClient
    {
        private ConnectionInfo connectionInfo;

        public Action<ulong> ProgressStatus;

        public SSHClient(string host, string username, string password, int port = 22)
        {
            Console.WriteLine("SSH configurations:");
            Console.WriteLine("Host: " + host);
            Console.WriteLine("Username: " + username);
            Console.WriteLine("Port: " + port);
            var authenticationMethod = new PasswordAuthenticationMethod(username, password);
            this.connectionInfo = new ConnectionInfo(host, port, username, authenticationMethod);
        }

        private string CreateConnection(Func<SshClient, String> action)
        {
            using (var client = new SshClient(this.connectionInfo))
            {
                try
                {
                    client.Connect();
                    var str = action.Invoke(client);
                    client.Disconnect();
                    return str;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("SSH configurations:");
                    Console.WriteLine("Host" + this.connectionInfo.Host);
                    Console.WriteLine("Username" + this.connectionInfo.Username);
                    Console.WriteLine("Port" + this.connectionInfo.Port);
                    throw ex;
                }
            }
        }

        public IList<string> ListFilePaths(string path)
        {
            var str = CreateConnection((client) =>
            {
                var command = client.RunCommand($"find {path}");
                return command.Result;
            });

            return str.Split('\n', StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public IList<string> ListFilePathsByExtension(string path, string extension)
        {
            var str = CreateConnection((client) => client.RunCommand($"find {path} -name \"*.{extension}\"").Result);
            return str.Split('\n', StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
