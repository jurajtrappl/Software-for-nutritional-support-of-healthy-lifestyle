using Application.Core.Common.Constants;
using Application.Web.Properties;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;

namespace Application.Web
{
    public sealed class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
    {
        private readonly IStringLocalizer _localizer;

        public LocalizedIdentityErrorDescriber(IStringLocalizer<IdentityErrorResources> localizer)
        {
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }

        public override IdentityError DuplicateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.StringIsNullOrEmpty, nameof(email)));
            }

            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = string.Format(_localizer[LocalizedIdentityErrorMessages.DuplicateEmail].Value, email)
            };
        }

        public override IdentityError DuplicateUserName(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.StringIsNullOrEmpty, nameof(username)));
            }

            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = string.Format(
                    _localizer[LocalizedIdentityErrorMessages.DuplicateUserName].Value, username)
            };
        }

        public override IdentityError InvalidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.StringIsNullOrEmpty, nameof(email)));
            }

            return new IdentityError
            {
                Code = nameof(InvalidEmail),
                Description = string.Format(_localizer[LocalizedIdentityErrorMessages.InvalidEmail].Value, email)
            };
        }

        public override IdentityError DuplicateRoleName(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.StringIsNullOrEmpty, nameof(role)));
            }

            return new IdentityError
            {
                Code = nameof(DuplicateRoleName),
                Description = string.Format(_localizer[LocalizedIdentityErrorMessages.DuplicateRoleName].Value, role)
            };
        }

        public override IdentityError InvalidRoleName(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.StringIsNullOrEmpty, nameof(role)));
            }

            return new IdentityError
            {
                Code = nameof(InvalidRoleName),
                Description = string.Format(_localizer[LocalizedIdentityErrorMessages.InvalidRoleName].Value, role)
            };
        }

        public override IdentityError InvalidToken()
        {
            return new IdentityError
            {
                Code = nameof(InvalidToken),
                Description = _localizer[LocalizedIdentityErrorMessages.InvalidToken].Value
            };
        }

        public override IdentityError InvalidUserName(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.StringIsNullOrEmpty, nameof(username)));
            }

            return new IdentityError
            {
                Code = nameof(InvalidUserName),
                Description = string.Format(_localizer[LocalizedIdentityErrorMessages.InvalidUserName].Value, username)
            };
        }

        public override IdentityError LoginAlreadyAssociated()
        {
            return new IdentityError
            {
                Code = nameof(LoginAlreadyAssociated),
                Description = _localizer[LocalizedIdentityErrorMessages.LoginAlreadyAssociated].Value
            };
        }

        public override IdentityError PasswordMismatch()
        {
            return new IdentityError
            {
                Code = nameof(PasswordMismatch),
                Description = _localizer[LocalizedIdentityErrorMessages.PasswordMismatch].Value
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = _localizer[LocalizedIdentityErrorMessages.PasswordRequiresDigit].Value
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresLower),
                Description = _localizer[LocalizedIdentityErrorMessages.PasswordRequiresLower].Value
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = _localizer[LocalizedIdentityErrorMessages.PasswordRequiresNonAlphanumeric].Value
            };
        }

        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUniqueChars),
                Description = string.Format(
                    _localizer[LocalizedIdentityErrorMessages.PasswordRequiresUniqueChars].Value, uniqueChars)
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                Description = _localizer[LocalizedIdentityErrorMessages.PasswordRequiresUpper].Value
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = nameof(PasswordTooShort),
                Description = string.Format(_localizer[LocalizedIdentityErrorMessages.PasswordTooShort].Value, length)
            };
        }

        public override IdentityError UserAlreadyHasPassword()
        {
            return new IdentityError
            {
                Code = nameof(UserAlreadyHasPassword),
                Description = _localizer[LocalizedIdentityErrorMessages.UserAlreadyHasPassword].Value
            };
        }

        public override IdentityError UserAlreadyInRole(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.StringIsNullOrEmpty, nameof(role)));
            }

            return new IdentityError
            {
                Code = nameof(UserAlreadyInRole),
                Description = string.Format(_localizer[LocalizedIdentityErrorMessages.UserAlreadyInRole].Value, role)
            };
        }

        public override IdentityError UserNotInRole(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.StringIsNullOrEmpty, nameof(role)));
            }

            return new IdentityError
            {
                Code = nameof(UserNotInRole),
                Description = string.Format(_localizer[LocalizedIdentityErrorMessages.UserNotInRole].Value, role)
            };
        }

        public override IdentityError UserLockoutNotEnabled()
        {
            return new IdentityError
            {
                Code = nameof(UserLockoutNotEnabled),
                Description = _localizer[LocalizedIdentityErrorMessages.UserLockoutNotEnabled].Value
            };
        }

        public override IdentityError RecoveryCodeRedemptionFailed()
        {
            return new IdentityError
            {
                Code = nameof(RecoveryCodeRedemptionFailed),
                Description = _localizer[LocalizedIdentityErrorMessages.RecoveryCodeRedemptionFailed].Value
            };
        }

        public override IdentityError ConcurrencyFailure()
        {
            return new IdentityError
            {
                Code = nameof(ConcurrencyFailure),
                Description = _localizer[LocalizedIdentityErrorMessages.ConcurrencyFailure].Value
            };
        }

        public override IdentityError DefaultError()
        {
            return new IdentityError
            {
                Code = nameof(DefaultError),
                Description = _localizer[LocalizedIdentityErrorMessages.DefaultIdentityError].Value
            };
        }
    }
}