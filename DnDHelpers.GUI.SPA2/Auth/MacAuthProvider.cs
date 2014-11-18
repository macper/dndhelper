using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DnDHelper.Domain;
using ServiceStack.ServiceInterface.Auth;

namespace DnDHelpers.GUI.SPA2.Auth
{
    public class MacAuthProvider : IUserAuthRepository
    {
        private static readonly ILogger Logger = ServiceContainer.GetInstance<ILogger>(new KeyValuePair<string, object>(Const.LoggerName, typeof(MacAuthProvider).Name));
        public string CreateOrMergeAuthSession(IAuthSession authSession, IOAuthTokens tokens)
        {
            throw new NotImplementedException();
        }

        public UserAuth CreateUserAuth(UserAuth newUser, string password)
        {
            throw new NotImplementedException();
        }

        public UserAuth GetUserAuth(IAuthSession authSession, IOAuthTokens tokens)
        {
            return new UserAuth
            {
                UserName = authSession.UserAuthName,
                DisplayName = authSession.DisplayName,
                Roles = authSession.Roles
            };
        }

        public UserAuth GetUserAuth(string userAuthId)
        {
            throw new NotImplementedException();
        }

        public UserAuth GetUserAuthByUserName(string userNameOrEmail)
        {
            throw new NotImplementedException();
        }

        public List<UserOAuthProvider> GetUserOAuthProviders(string userAuthId)
        {
            return new List<UserOAuthProvider>();
        }

        public void LoadUserAuth(IAuthSession session, IOAuthTokens tokens)
        {
            throw new NotImplementedException();
        }

        public void SaveUserAuth(UserAuth userAuth)
        {
            throw new NotImplementedException();
        }

        public void SaveUserAuth(IAuthSession authSession)
        {
            throw new NotImplementedException();
        }

        public bool TryAuthenticate(Dictionary<string, string> digestHeaders, string PrivateKey, int NonceTimeOut, string sequence, out UserAuth userAuth)
        {
            throw new NotImplementedException();
        }

        public bool TryAuthenticate(string userName, string password, out UserAuth userAuth)
        {
            try
            {
                var authService = new MacAuthService.Contracts.SimpleServiceProxy();
                if (authService.Authorize(userName, password).IsSuccessfull)
                {
                    var roles = authService.GetRoles(userName);
                    userAuth = new UserAuth
                    {
                        DisplayName = userName,
                        UserName = userName,
                        Roles = roles.Select(s => s.Name).ToList()
                    };
                    Logger.Info("Zalogowano: " + userName);
                    return true;
                }
            }
            catch (Exception exc)
            {
                Logger.Error("Błąd podczas logowania: " + exc.Message, exc);
            }
            userAuth = null;
            return false;
        }

        public UserAuth UpdateUserAuth(UserAuth existingUser, UserAuth newUser, string password)
        {
            throw new NotImplementedException();
        }
    }
}
