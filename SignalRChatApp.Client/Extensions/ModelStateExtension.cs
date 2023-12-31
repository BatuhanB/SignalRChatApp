﻿using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SignalRChatApp.Client.Extensions
{
    public static class ModelStateExtension
    {
        public static void AddModelErrorList(this ModelStateDictionary modelState,List<string> errors)
        {
            foreach (var error in errors)
            {
                modelState.AddModelError(string.Empty, error);
            }
        }
    }
}
