using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Yandex.Money.Api.Sdk.Interfaces;

namespace Yandex.Money.Api.Sdk.Utils
{
    /// <summary>
    /// The generic serializer to and from JSON.
    /// </summary>
    public class JsonSerializer<TResult> : IGenericSerializer<TResult>
    {
        /// <summary>Null value handling style.
        /// </summary>
        public NullValueHandling NullValueHandling { get; set; }

        public TResult Deserialize(Stream stream, bool leaveOpen = false)
        {
            // do not dispose StreamReader as it would dispose the stream
            // if leaveOpen is false, JsonTextReader would dispose StreamReader just fine

            var streamReader = new StreamReader(stream);

            using (var jsonTextReader = new JsonTextReader(streamReader) { CloseInput = !leaveOpen })
            {
                return CreateSerializer()
                    .Deserialize<TResult>(jsonTextReader);
            }
        }

        public TResult Deserialize(string value)
        {
            var jsonSerializerSettings = GetSerializerSettings();
            return JsonConvert.DeserializeObject<TResult>(value, jsonSerializerSettings);
        }

        public void Serialize(TResult value, Stream outStream)
        {
            using (var streamWriter = new StreamWriter(outStream))
            {
                CreateSerializer().Serialize(streamWriter, value);
            }
        }

        public string Serialize(TResult value)
        {
            return JsonConvert.SerializeObject(
                value,
                GetSerializerSettings());
        }

        /// <summary>
        /// Override this if you need to alter serializer settings.
        /// </summary>
        /// <returns>JsonSerializerSettings which is used in every serializing and deserializing operation.</returns>
        /// <remarks>If you need to fine-tune serializer, please override <see cref="CreateSerializer"/>.</remarks>
        protected virtual JsonSerializerSettings GetSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling,
				Converters = new JsonConverter[]
				{
					new StringEnumConverter()
				}
            };
        }

        /// <summary>
        /// Creates JsonSerializer instance and applies GetSerializerSettings() settings.
        /// </summary>
        /// <remarks>This does not affect stringly <see cref="Deserialize(string)"/> or <see cref="Serialize(TResult)"/></remarks>
        protected virtual JsonSerializer CreateSerializer()
        {
            JsonSerializerSettings settings = GetSerializerSettings();

            var result = new JsonSerializer
            {
                TypeNameHandling = settings.TypeNameHandling,
            };

            if (settings.Converters == null) 
                return result;

            foreach (var converter in settings.Converters)
                result.Converters.Add(converter);

            return result;
        }
    }
}
