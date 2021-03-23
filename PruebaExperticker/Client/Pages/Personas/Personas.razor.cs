using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Json;
using MatBlazor;
using PruebaExperticker.Shared;

namespace PruebaExperticker.Client.Pages.Personas
{
    public partial class Personas
    {
        [Inject]
        protected IMatToaster Toaster { get; set; }

        protected IEnumerable<Persona> personas { get; set; }

        protected bool loading { get; set; } = false;

 
        protected override async Task OnInitializedAsync()
        {

            //Al iniciar componente se hace petición a la API 
            await FetchData();
        }

        protected async Task Borrar(string dni) {
            try
            {

                var result = await MatDialogService.AskAsync("¿Estás seguro de que quieres borrar esta persona?", new string[] { "Sí", "No"});
                if (result=="Sí")
                {
                    await Http.DeleteAsync("api/persona/" + dni);
                    await FetchData();
                    StateHasChanged();
                    Toaster.Add("Persona borrada!", MatToastType.Success, "Exito", MatIconNames.Check_circle);
                }

            }
            catch
            {
                Toaster.Add("Fallo al borrar persona", MatToastType.Danger, "Error", MatIconNames.Clear);
            }
        }
        async Task OpenAddPersonaDialog()
        {
            await MatDialogService.OpenAsync(typeof(AddPersona), null);
            await FetchData();
            StateHasChanged();
        }        
        async Task OpenUpdatePersonaDialog(string DNI)
        {
            await MatDialogService.OpenAsync(typeof(UpdatePersona), new MatDialogOptions { Attributes = new Dictionary<string, object>(){{"DNI",DNI} } });
            await FetchData();
            StateHasChanged();
        }
        protected async Task FetchData() {
            loading = true;
            try
            {
                var res = await Http.GetFromJsonAsync<List<Persona>>("api/persona");
                personas = res;
            }
            catch
            {
                Toaster.Add("Compruebe conexión y reintente", MatToastType.Danger, "Error", MatIconNames.Error);

            }
            loading = false;
        }
    }
}
