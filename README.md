[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/xxk7tiN_)
# Ejercicio N° 15
## Desarrollo de Software

1. Clonar el repositorio creado al aceptar la asignación
2. Crear una rama de larga duración denominada __development__
3. Desarrollar una API Web basada en controladores en .NET, con las siguientes características:
   1. Nombre de la solución: `Dsw2026Ej15`
   2. Capa de servicios distribuidos (Api) en proyecto: `Dsw2026Ej15.Api`
   3. Capa de dominio (Domain) en proyecto: `Dsw2026Ej15.Domain`
   4. Capa de persistencia de datos (Data) en proyecto: `Dsw2026Ej15.Data`
   5. El dominio tiene las siguientes entidades:
      1. __Speciality__
         * Name (string)
         * Description (string)
      2. __Doctor__
         * Name (string)
         * LicenseNumber (string)
         * IsActive (bool)
         * Speciality (Speciality)
      3. __BaseEntity__ (abstract)
         * Id (Guid)
   6. La capa de persistencia cuenta con la clase `PersistenceInMemory` (registrar como Singleton), la clase debe:
      1. Ser la implementación de la abstracción de persistencia (IPersistence)
      2. Mantener, mientras se ejecuta la aplicación, los datos de médicos (doctors) y especialidades (specialities)
      3. Los métodos necesarios para gestionar los datos de médicos y especialidades (definir según necesidades)
      4. Un método (privado) `LoadSpecialities()` que obtiene las especialidades de un archivo JSON (proporcionado)
   7. Agregar el controlador `DoctorsController` con 4 (cuatro) endpoints:
      1. __Primer endpoint__:
         * Método: __POST__
         * Ruta: api/doctors
         * Descripción: insertar un nuevo médico
         * Body
            ```
            {
               “name”: “string”,
               “licenseNumber”: “string”,
               “specialityId”: “Guid”
            }
            ```
         * Validaciones:
           * Name. requerido
           * LicenseNumber: requerido
           * SpecialityId: debe existir
         * Comportamiento: se crea activo
         * Respuesta exitosa: `201` Created
         * Respuesta de error: `400` BadRequest (con el mensaje indicando el motivo)
      2. __Segundo endpoint__:
         * Método: __GET__
         * Ruta: api/doctors
         * Descripción: obtener todos los médicos activos
         * Respuesta exitosa: `200` OK con la colección de médicos (incluso si no hay médicos activos)
      3. __Tercer endpoint__:
         * Método: __GET__
         * Ruta: api/doctors/{id}
         * Descripción: obtener un médico activo a partir de su Id
         * Validación: el médico debe existir y estar activo
         * Respuesta exitosa: `200` OK con los datos del médico 
           - Name
           - LicenseNumber 
           - SpecialityName
         * Respuesta de error: `404` Not Found si no se encuentra el médico o no está activo
      4. __Cuarto endpoint__:
         * Método: __DELETE__
         * Ruta: api/doctors/{id}
         * Descripción: establecer como inactivo al médico
         * Validación: el médico debe existir y estar activo
         * Respuesta exitosa: `204` No Content
         * Respuesta de error: `404` Not Found si no se encuentra el médico o no está activo
   8. Si no se superan las validaciones se deberán generar excepciones del tipo ValidationException (nueva clase).
   9. Agregar un middleware para controlar las excepciones:
      * Cuando sean del tipo `ValidationException` se debe retornar el código `400` Bad Request
      * En cualquier otro caso se debe retornar el código `500` Problem
   10. Agregar sondeo de estado básico en la ruta __/health-check__
4. Al terminar subir la rama __development__ al repositorio remoto y actualizar la rama __main__ mediante un __pull-request__

- Requisito:
__Aplicar principios y patrones estudiados.__

- Tip:
Código de ejemplo para importar los productos
  ```
  var json = await File.ReadAllTextAsync("products.json");
  var products = JsonSerializer.Deserialize<List<Product>>(json);
  ```
