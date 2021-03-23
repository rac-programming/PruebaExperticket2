using PruebaExperticker.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using MatBlazor;

namespace PruebaExperticker.Client.Pages.Personas
{
    public partial class UpdatePersona
    {
        [Parameter]
        public string DNI { get; set; }

        [Inject]
        protected IMatToaster Toaster { get; set; }

        [CascadingParameter]
        public MatDialogReference DialogReference { get; set; }

        public Persona persona = new Persona();
        public string error;
        public bool loading;

        protected override async Task OnInitializedAsync()
        {

            //Al iniciar componente se hace petición a la API
            try
            {
                var res = await Http.GetFromJsonAsync<Persona>("api/persona/"+DNI);
                persona = res;

            }
            catch
            {
                Toaster.Add("Compruebe conexión y reintente", MatToastType.Danger, "Error", MatIconNames.Error);
               
            }
        }
        protected async Task Submit()
        {
            loading = true;
            try
            {
                error = "";
                var per = persona;
                var res = await Http.PutAsJsonAsync("api/persona", per);
                await CloseDialog();
                Toaster.Add("Persona modificada!", MatToastType.Success, "Exito", MatIconNames.Check_circle);
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
