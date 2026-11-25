[Diagrama UML.pdf](https://github.com/user-attachments/files/35411230/Diagrama.UML.pdf)
//Registro de asistencia laboral//

En una empresa se desea llevar de forma automatizada el registro de asistencia de los trabajadores, para ello se desea crear un registro automatizado en la cual
los trabajadores ingresan su hora de entrada y de salida de la empresa y el sistema se encarga de pasar el reporte al área de gestión humana.

FUNCIONALIDADES
1. Registrar empleados 
Qué hace: Permite agregar un trabajador al sistema pidiendo: documento, nombre, apellido, fecha de nacimiento y cargo.

2. Eliminar / Inactivar empleados — Únicamente autorizados 
Qué hace: Un usuario autorizado de Gestión Humana (con usuario y clave) puede retirar/eliminar un empleado de la lista activa.
Cómo se conecta con tus requisitos:
NOTA: Se tendra la clase base Persona, de ella hereda Empleado, y AdministradorRH hereda sus permisos especiales
(sobrescribiendo el método para autorizar).
Si alguien sin permiso intenta hacerlo, el sistema lo rechaza con un mensaje claro.

4. Registrar hora de llegada 
Qué hace: El trabajador ingresa su documento (y el sistema busca y muestra su nombre y apellido automáticamente para confirmar que sí existe y está activo).
Hora automática: Al presionar "Registrar Llegada", el sistema toma la hora exacta del computador (DateTime.Now) sin dejar que el usuario la modifique.
Validación (registros rechazados): Si el documento no existe, está inactivo, o ya marcó llegada ese día sin haber salido, el sistema lo rechaza,
muestra alerta y cuenta ese intento como registro rechazado para el informe.

6. Registrar hora de salida 
Qué hace: El trabajador ingresa su documento, el sistema confirma su nombre y apellido y registra automáticamente la hora exacta de salida (DateTime.Now).
Validación: No permite registrar salida si no tiene una llegada abierta previa (si lo intenta, se cuenta como registro rechazado).

7. Evaluar horas laboradas diurnas y nocturnas + Reporte a Gestión Humana 
Qué hace: Al registrar la salida, el sistema calcula automáticamente cuántas horas fueron diurnas (6:00 a. m. a 7:00 p. m.) y
cuántas nocturnas (7:00 p. m. a 6:00 a. m.).
