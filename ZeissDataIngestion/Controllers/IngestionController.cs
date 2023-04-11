using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZeissDataIngestion.Model;
using ZeissDataIngestion.Repository;

namespace ZeissDataIngestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngestionController : ControllerBase
    {
        private readonly ILogger<IngestionController> _logger;
        private readonly ClientWebSocket _webSocket;
        private readonly IMachineDataRepository _repository;

        public IngestionController(IMachineDataRepository repository,ILogger<IngestionController> logger)
        {
            _logger = logger;
            _repository = repository;
            _webSocket = new ClientWebSocket();
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                // Connect to the websocket endpoint
                var uri = new Uri("wss://codingcase.bluesky-ff1656b7.westeurope.azurecontainerapps.io/ws/websocket");
                await _webSocket.ConnectAsync(uri, cancellationToken);

                var buffer = new byte[1024 * 4];
                var sb = new StringBuilder();

                while (_webSocket.State == WebSocketState.Open)
                {
                    // Receive data from the websocket
                    var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", cancellationToken);
                        break;
                    }

                    // Convert the received data to a JSON object and store it
                    var json = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    sb.Append(json);

                    if (result.EndOfMessage)
                    {
                        var message = JObject.Parse(sb.ToString());
                        MachineData machineData = message.ToObject<MachineData>();
                        sb.Clear();

                        // Store the data to Cosmos DB or perform any other action here
                        await _repository.AddDataToCosmosAsync(message);
                        _logger.LogInformation("Received data from websocket: {0}", message);
                    }
                }

                return Ok();
            }
            catch (WebSocketException ex)
            {
                _logger.LogError("WebSocket error: {0}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    
    }
}
