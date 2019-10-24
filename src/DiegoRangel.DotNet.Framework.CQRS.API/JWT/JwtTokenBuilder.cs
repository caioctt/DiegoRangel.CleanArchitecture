using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace DiegoRangel.DotNet.Framework.CQRS.API.JWT
{
    public sealed class JwtTokenBuilder
    {
        private SecurityKey _securityKey;
        private string _subject = "";
        private string _issuer = "";
        private string _audience = "";
        private string _uniqueName = "";
        private int _expiryInMinutes = 60;
        private readonly Dictionary<string, string> _claims = new Dictionary<string, string>();
        private readonly IList<string> _roles = new List<string>();

        public JwtTokenBuilder AddSecurityKey(SecurityKey securityKey)
        {
            _securityKey = securityKey;
            return this;
        }
        public JwtTokenBuilder AddSubject(string subject)
        {
            _subject = subject;
            return this;
        }
        public JwtTokenBuilder AddIssuer(string issuer)
        {
            _issuer = issuer;
            return this;
        }
        public JwtTokenBuilder AddAudience(string audience)
        {
            _audience = audience;
            return this;
        }
        public JwtTokenBuilder AddUniqueName(string uniqueName)
        {
            _uniqueName = uniqueName;
            return this;
        }
        public JwtTokenBuilder AddClaim(string type, string value)
        {
            _claims.Add(type, value);
            return this;
        }
        public JwtTokenBuilder AddClaims(Dictionary<string, string> claims)
        {
            _claims.Union(claims);
            return this;
        }
        public JwtTokenBuilder AddExpiry(int expiryInMinutes)
        {
            _expiryInMinutes = expiryInMinutes;
            return this;
        }
        public JwtTokenBuilder AddRole(string role)
        {
            _roles.Add(role);
            return this;
        }

        public string Build()
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.Sub, _subject),
                new Claim(JwtRegisteredClaimNames.UniqueName, _uniqueName)
            }
            .Union(_claims.Select(item => new Claim(item.Key, item.Value)))
            .Union(_roles.Select(role => new Claim(ClaimTypes.Role, role)))
            .ToList();

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.UtcNow.AddMinutes(_expiryInMinutes),
                signingCredentials: new SigningCredentials(
                    _securityKey,
                    SecurityAlgorithms.HmacSha256
                ));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}