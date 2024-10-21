using AppWebFurryFriendsHub.Shared;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using System.Text;

namespace AppWebFurryFriendsHub.Client.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User> AuthenticateUser(UserAuthDTO userAuthDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user/Login", userAuthDTO);

            try
            {
                if (response.IsSuccessStatusCode)
                {

                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var user = JsonSerializer.Deserialize<User>(jsonResponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return user;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException("Usuario o contraseña incorrectos");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error inesperado: {response.ReasonPhrase}. Detalles: {errorResponse}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Manejo de errores de red
                throw new Exception("Error en la solicitud HTTP: " + ex.Message);
            }
            catch (JsonException ex)
            {
                // Manejo de errores de deserialización
                throw new Exception("Error al deserializar la respuesta: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la solicitud HTTP: " + ex.Message);
            }

        }

        public async Task DeleteUser(string id)
        {
            _httpClient.DeleteAsync($"api/user/{id}");
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<User>>
            (await _httpClient.GetStreamAsync($"api/user"),
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<User> GetUserDetail(string id)
        {
            return await JsonSerializer.DeserializeAsync<User>
            (await _httpClient.GetStreamAsync($"api/user/{id}"),
             new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task SaveUser(User user)
        {
            var userJson = new StringContent(JsonSerializer.Serialize(user),
                Encoding.UTF8, "application/json");

            HttpResponseMessage response;

            if (string.IsNullOrEmpty(user.Id))
            {
                response = await _httpClient.PostAsync($"api/user", userJson);
            }
            else
            {
                response = await _httpClient.PutAsync($"api/user/{user.Id}", userJson);
            }


            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Usuario guardado con éxito.");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al guardar el usuario: {response.StatusCode}, Detalles: {errorContent}");
            }
        }
    }
}
