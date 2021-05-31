using System;
using System.Reflection;
using System.Runtime.Versioning;

namespace FunctionAppContagem.Models
{
    public class ResultadoContador
    {
        private static readonly string _SAUDACAO;
        private static readonly string _AVISO;
        private static readonly string _LOCAL;
        private static readonly string _KERNEL;
        private static readonly string _TARGET_FRAMEWORK;

        static ResultadoContador()
        {
            _SAUDACAO = Environment.GetEnvironmentVariable("Saudacao");
            _AVISO = Environment.GetEnvironmentVariable("Aviso");
            _LOCAL = Environment.MachineName;
            _KERNEL = Environment.OSVersion.VersionString;
            _TARGET_FRAMEWORK = Assembly
                .GetEntryAssembly()?
                .GetCustomAttribute<TargetFrameworkAttribute>()?
                .FrameworkName;
        }

        public int ValorAtual { get; init; }
        public string Saudacao { get => _SAUDACAO; }
        public string Aviso { get => _AVISO; }
        public string Local { get => _LOCAL; }
        public string Kernel { get => _KERNEL; }
        public string TargetFramework { get => _TARGET_FRAMEWORK; }

        public ResultadoContador(int valorAtual)
        {
            ValorAtual = valorAtual;
        }
    }
}