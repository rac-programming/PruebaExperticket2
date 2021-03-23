using PruebaExperticker.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Json;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace PruebaExperticker.Client.Pages.Personas
{
    public partial class AddPersona
    {
        public Persona persona = new Persona();
        public string error;
        public bool loading;

        [Inject]
        protected IMatToaster Toaster { get; set; }

        [CascadingParameter]
        public MatDialogReference DialogReference { get; set; }

        protected async Task Submit()
        {
            loading = true;
            try
            {
                error = "";
                var per = persona;
                var res = await Http.PostAsJsonAsync("api/persona", per);
                await CloseDialog();
                Toaster.Add("Persona añadida!", MatToastType.Success, "Exito", MatIconNames.Check_circle);
            }
            catch
            {
                error = "Compruebe la conexión y vuelva a intentarlo";
                this.StateHasChanged();
            }
            loading = false;
        }
        async Task CloseDialog()
        {
            DialogReference.Close("Test");
        }
    }
}
