﻿using System.ComponentModel.DataAnnotations;

namespace Common.Api
{
    public enum ApiResultStatusCode
    {
        [CustomDisplayStatusAttribute("Operation Failed")]
        UnSuccess = 0,

        [CustomDisplayStatusAttribute("Operation Successful", true)]
        Success = 1,

        [CustomDisplayStatusAttribute("An error occurred")]
        UnHandledError = 2,

        [CustomDisplayStatusAttribute("Invalid sent parameters")]
        BadRequest = 3,

        [CustomDisplayStatusAttribute("Not Found")]
        NotFound = 4,

        [CustomDisplayStatusAttribute("Authentication Error")]
        UnAuthorized = 5,

        [CustomDisplayStatusAttribute("This item has already been registered")]
        EntityExists = 6,

        [CustomDisplayStatusAttribute("This user has already been registered")]
        ExistUser = 7,

        [CustomDisplayStatusAttribute("The specified user does not exist")]
        UserNotExist = 8,

        [CustomDisplayStatusAttribute("Incorrect password")]
        WrongPassword = 9,

        [CustomDisplayStatusAttribute("Sent parameters are invalid")]
        ParameterIsNotExist = 10,

        [CustomDisplayStatusAttribute("Authentication failed")]
        AuthenticateFailure = 11,

        [CustomDisplayStatusAttribute("Access Denied")]
        DontAllowAccessThisResource = 12,

        [CustomDisplayStatusAttribute("Item not found")]
        EntityNotFound = 13,

        [CustomDisplayStatusAttribute("This username has already been chosen")]
        UserNameExist = 14,

        [CustomDisplayStatusAttribute("This email has already been chosen")]
        EmailExist = 15,

        [CustomDisplayStatusAttribute("Username is required")]
        UserNameIsEmpty = 16,

        [CustomDisplayStatusAttribute("Nickname is required")]
        NickNameIsEmpty = 17,

        [CustomDisplayStatusAttribute("Password is required")]
        PasswordIsEmpty = 18,

        [CustomDisplayStatusAttribute("Username cannot contain spaces")]
        UserNameHasSpace = 19,

        [CustomDisplayStatusAttribute("Registration completed successfully", true)]
        RegistrationCompleted = 20,

        [CustomDisplayStatusAttribute("Login completed successfully", true)]
        LoginCompleted = 21,

        [CustomDisplayStatusAttribute("Password cannot contain spaces")]
        PasswordHasSpace = 22,

        [CustomDisplayStatusAttribute("Vocabulary added successfuly", true)]
        VocabularyAdded = 23,

        [CustomDisplayStatusAttribute("Vocabulary updated successfuly", true)]
        VocabularyUpdated = 24,

        [CustomDisplayStatusAttribute("Vocabulary removed successfuly", true)]
        VocabularyRemoved = 25,

        [CustomDisplayStatusAttribute("You have already added this vocabulary.")]
        VocabularyAlreadyAdded = 26,

        [CustomDisplayStatusAttribute("The word or its meaning is requied")]
        VocabularyWordMeaningIsRequied = 27,
    }
}