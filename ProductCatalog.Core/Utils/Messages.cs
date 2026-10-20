namespace ProductCatalog.Core.Utils {
    public static class Messages {
        public static string UnexpectedErrorMessage => "An unexpected error occurred.";
        public static string StartedRetrievingAllLogMessage => "Started retrieving all {entityNamePluralLower}.";
        public static string SuccessfullyRetrievedAllLogMessage => "Successfully retrieved all {entityNamePluralLower}.";
        public static string UnexpectedErrorOccurredRetrievingAllLogMessage => "Unexpected error occurred while retrieving all {entityNamePluralLower}. Exception message: {message}.";
        public static string StartedRetrievingEntityWithIdLogMessage => "Started retrieving {entityNameSingularLower} with Id={id}.";
        public static string EntityWithIdNotFoundLogMessage => "{entityNameSingularUpper} with Id = {id} was not found.";
        public static string SuccessfullyRetrievedEntityWithIdLogMessage => "Successfully retrieved {entityNameSingularLower} with Id = {id}";
        public static string UnexpectedErrorOccurredRetrievingEntityWithIdLogMessage => "Unexpected error occurred while retrieving {entityNameSingularLower} with Id = {id}. Exception message: {message}.";
        public static string StartedCreatingEntityLogMessage => "Started creating {entityNameSingularLower}.";
        public static string SuccessfullyCreatedEntityWithIdLogMessage => "Successfully created {entityNameSingularLower} with Id = {id}.";
        public static string UnexpectedErrorOccurredCreatingEntityLogMessage => "Unexpected error occurred while creating {entityNameSingularLower}. Exception message: {message}.";
        public static string StartedUpdatingEntityWithIdLogMessage => "Started updating {entityNameSingularLower} with Id = {id}.";
        public static string SuccessfullyUpdatedEntityWithIdLogMessage => "Successfully updated {entityNameSingularLower} with Id = {id}.";
        public static string UnexpectedErrorOccurredUpdatingEntityWithIdLogMessage => "Unexpected error occurred while updating {entityNameSingularLower} with Id = {id}. Exception message: {message}.";
        public static string StartedDeletingEntityWithIdLogMessage => "Started deleting {entityNameSingularLower} with Id = {id}.";
        public static string SuccessfullyDeletedEntityWithIdLogMessage => "Successfully deleted {entityNameSingularLower} with Id = {id}.";
        public static string UnexpectedErrorOccurredDeletingEntityWithIdLogMessage => "Unexpected error occurred while deleting {entityNameSingularLower} with Id = {id}. Exception message: {message}.";
        public static string EntityCreationRequestFailedValidationLogMessage => "{entityNameSingularUpper} creation request failed validation: {message}";
        public static string EntityUpdatingRequestFailedValidationLogMessage => "{entityNameSingularUpper} updating request failed validation: {message}";

        public static string FormatEntityWithIdNotFoundErrorMessage(string entityNameSingularUpper, int id) {
            return $"{entityNameSingularUpper} with Id = {id} was not found.";
        }
    }
}
