namespace ProductCatalog.Core.Utils {
    public static class Messages {
        public static string UnexpectedErrorMessage => "An unexpected error occurred while processing the request.";
        public static string RequestValidationFailedMessage => "Request validation failed.";
        public static string EntityNotFoundMessage => "Entity not found.";
        public static string StartedRetrievingAllMessage => "Started retrieving all {entityNamePluralLower}.";
        public static string SuccessfullyRetrievedAllMessage => "Successfully retrieved all {entityNamePluralLower}.";
        public static string StartedRetrievingEntityWithIdMessage => "Started retrieving {entityNameSingularLower} with Id={id}.";
        public static string EntityWithIdNotFoundMessage => "{entityNameSingularUpper} with Id = {id} was not found.";
        public static string SuccessfullyRetrievedEntityWithIdMessage => "Successfully retrieved {entityNameSingularLower} with Id = {id}";
        public static string StartedCreatingEntityMessage => "Started creating {entityNameSingularLower}.";
        public static string SuccessfullyCreatedEntityWithIdMessage => "Successfully created {entityNameSingularLower} with Id = {id}.";
        public static string StartedUpdatingEntityWithIdMessage => "Started updating {entityNameSingularLower} with Id = {id}.";
        public static string SuccessfullyUpdatedEntityWithIdMessage => "Successfully updated {entityNameSingularLower} with Id = {id}.";
        public static string StartedDeletingEntityWithIdMessage => "Started deleting {entityNameSingularLower} with Id = {id}.";
        public static string SuccessfullyDeletedEntityWithIdMessage => "Successfully deleted {entityNameSingularLower} with Id = {id}.";

        public static string FormatEntityWithIdNotFoundErrorMessage(string entityNameSingularUpper, int id) {
            return $"{entityNameSingularUpper} with Id = {id} was not found.";
        }
        public static string FormatEntityRequestFailedValidationErrorMessage(string entityNameSingularUpper, string action, string message) {
            return $"{entityNameSingularUpper} {action} request failed validation: {message}";
        }
    }
}
