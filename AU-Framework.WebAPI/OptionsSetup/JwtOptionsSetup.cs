using AU_Framework.Inftrastructure.Authentication;
using Microsoft.Extensions.Options;

namespace AU_Framework.WebAPI.OptionsSetup;

    public sealed class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        private readonly IConfiguration _configuration;

        public JwtOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(JwtOptions options)
        {
            _configuration.GetSection("Jwt").Bind(options);
        }
    }

