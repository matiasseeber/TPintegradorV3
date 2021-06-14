create database TpIntegradorProgramacion
go 

use TpIntegradorProgramacion
go

create table Categorias
(
idCategoria int identity (1,1) not null,
descripcionCategoria varchar(50) not null
constraint PK_Categorias primary key (idCategoria)
)
go

Create table Articulos
(
idArticulo int identity(1,1) not null,
idCategoria int null,
DescripcionArticulo varchar(50) not null,
precioUnitarioArticulo decimal(10,1) not null,
stockDisponibleArticulo int not null,
url_articulo_img varchar(50) not null,
estado bit,
constraint PK_articulos primary key (idArticulo),
constraint FK_articulos foreign key (idCategoria)
references Categorias (idCategoria)
)
go

create table Usuarios
(
dniUsuario varchar(15) not null,
NombreUsuario varchar(30) not null,
ApellidoUsuario varchar(30) not null,
emailUsuario varchar(50) unique not null,
direccionUsuario varchar(50) not null,
numTarjetaCredito varchar(20) not null,
codSeguridad varchar(3) not null,
adminEstadoUsuario bit not null,
adminMaster bit null,
contraseña varchar(25),
estado bit,
constraint PK_Usuarios primary key (dniUsuario)
)
go

Create table facturas
(
id_Factura int identity (1,1) not null,
dni_Usuario varchar(15) not null,
fecha_venta date not null,
monto_final decimal(30,5) not null
constraint PK_facturas primary key (id_Factura)
constraint FK_facturas foreign key (dni_Usuario)
references usuarios (dniUsuario)
)
go

create table detalleFacturas
(
id_factura int not null,
numeroDeOrden int identity (1,1) not null,
id_articulo int not null,
precio_unitario decimal (30,1) not null,
descripcionProducto varchar(50) not null,
cantidad int not null,
constraint PK_detalleFacturas primary key(id_factura,numeroDeOrden),
constraint FK_detalleFacturasFacturas foreign key (id_factura)
references Facturas (id_Factura),
constraint FK_detalleFacturasArticulo foreign key (id_articulo)
references Articulos (idArticulo)
)
go

create procedure AgregarArticulo
	@idCat int,
	@Descripcion varchar(50),
	@precio decimal(10,1),
	@stock int,
	@urlImg varchar(50)
	as
	insert into Articulos (idCategoria,DescripcionArticulo,precioUnitarioArticulo,stockDisponibleArticulo,url_articulo_img,estado)
	values (@idCat,@Descripcion,@precio,@stock,@urlImg,1)
	go

create procedure eliminarArticulo
	@id int
	as
	delete from Articulos where idArticulo = @id
	go

	create procedure modificarArticulo
	@id int,
	@idCat int,
	@descripcion varchar(50),
	@precio decimal(10,1),
	@stock int
	as
	update Articulos set DescripcionArticulo = @descripcion,idCategoria = @idCat, precioUnitarioArticulo = @precio, stockDisponibleArticulo = @stock where idArticulo = @id
	go

	create procedure restarStockArticulo
	@id int,
	@stockVendido int
	as
	update Articulos set stockDisponibleArticulo = stockDisponibleArticulo - @stockVendido where idArticulo = @id
	go

	/*CATEGORIAS*/

Create procedure AgregarCategoria
@Descripcion varchar(50)
as
insert into Categorias (descripcionCategoria)
values (@Descripcion)
go
---
Create procedure EliminarCategoria
@IdCat int
as
delete from Categorias where idCategoria=@IdCat
go

---

Create procedure ModificarCateogoria
@IdCat int,
@DescripcionCat varchar(50)
as
update Categorias set descripcionCategoria=@DescripcionCat where idCategoria=@IdCat
go

/*USUARIOS*/

create procedure agregarUsuario
@dni varchar(15),
@nombre varchar(30),
@apellido varchar(30),
@mail varchar(50),
@direccion varchar(50),
@numTarjetaCredito varchar(20),
@codigoSeguridad varchar(3),
@contra varchar(25)
as
insert into Usuarios (dniUsuario,NombreUsuario,ApellidoUsuario,emailUsuario,contraseña,direccionUsuario,numTarjetaCredito,codSeguridad,adminEstadoUsuario,adminMaster,estado)
values (@dni,@nombre,@apellido,@mail,@contra,@direccion,@numTarjetaCredito,@codigoSeguridad,0,0,1)
go

create procedure modifcarUsuario
@dni varchar(15),
@nuevaDireccion varchar(50),
@nuevaTarjetaCredito varchar(20)
as
update usuarios set direccionUsuario = @nuevaDireccion, numTarjetaCredito = @nuevaTarjetaCredito where dniUsuario = @dni
go

Create procedure eliminarUsuario
@dni varchar(15)
as
delete from Usuarios where dniUsuario = @dni
go

create procedure modificarEstadoAdminUsuario
@dni varchar(15),
@admin bit
as
update Usuarios set adminEstadoUsuario = @admin where dniUsuario = @dni 
go

/*FACTURAS*/

CREATE procedure agregarFactura
@dniUsuario varchar(15),
@montoFinal decimal(30,2)
as
insert into facturas (dni_Usuario,fecha_venta,monto_final)
values (@dniUsuario,(SELECT CONVERT(VARCHAR(19), GETDATE(), 120)),@montoFinal)
go


/*DETALLE DE FACTURAS*/

create procedure AgregarDetalleFacturas
@id_Factura int,
@id_Articulo int,
@PrecioUnitario decimal (30,1),
@descripcionProducto varchar (50),
@cantidad int
as 
insert into detalleFacturas(id_factura,id_articulo,precio_unitario, DescripcionProducto,Cantidad)
values (@id_Factura,@id_Articulo,@PrecioUnitario ,@descripcionProducto,@cantidad)
update Articulos set stockDisponibleArticulo = stockDisponibleArticulo - @cantidad where idArticulo = @id_Articulo
go

create procedure modificarEstadoAdminMaster
@dni int,
@master bit
as
update Usuarios set adminMaster = @master where dniUsuario = @dni
go


create procedure bajaLogicaUsuario
@dni varchar(15)
as
update Usuarios set estado = 0 where dniUsuario = @dni
go

create procedure bajaLogicaArticulo
@id int
as
update Articulos set estado = 0 where idArticulo = @id
go

exec agregarUsuario 4,'Benjamin', 'Monzalvo','benjamin@gmail.com','benavidez','123456','123','123'

exec agregarUsuario 5,'Ivan', 'Antunez','Ivan@gmail.com','benavidez','123456','123','123'

exec agregarUsuario 6,'Tamara', 'Herrera','tamara@gmail.com','moron','123456','123','123'

exec agregarUsuario 7,'Claudio','Fernandez','claudio@gmail.com','av.libertador','123','123','123'

exec modificarEstadoAdminUsuario 6,1
exec modificarEstadoAdminUsuario 7,1

exec modificarEstadoAdminMaster 6,7
exec modificarEstadoAdminMaster 7,7

insert into Categorias (descripcionCategoria)
select 'lacteos' union 
select 'congelados' union 
select 'bebidas' union  
select 'almacen' union   
select 'frutas y verduras' union   
select 'limpieza'union 
select 'electrodomesticos' union   
select 'perfumeria' union   
select 'quesos y fiambres'

exec AgregarCategoria'carnes y pescados'

alter table articulos
alter column preciounitarioarticulo decimal(10,1)

insert into articulos(idcategoria,DescripcionArticulo,precioUnitarioArticulo,stockDisponibleArticulo,url_articulo_img)
select 9,'Otito Dulce de batata a la vainilla 500gr',90,233,'~/QuesosFiambres/49.png'union
select 9,'Esnaola Dulce de batata 500gr',200,201,'~/QuesosFiambres/50.jpg'union
select 9,'Dulcor Dulce de batata con chocolate 500gr',210,65,'~/QuesosFiambres/51.jpg'union
select 9,'Orieta Dulce de membrillo 500gr',95,98,'~/QuesosFiambres/52.jpg'union
select 9,'Excelencia Aceitunas deshuesadas 250gr',360,17,'~/QuesosFiambres/53.jpg'union
select 9,'Alfa Encurtido en vinagre 500gr',315,54,'~/QuesosFiambres/54.jpg'union
select 9,'Lorente Aceitunas sin hueso 2kg',710,87,'~/QuesosFiambres/55.jpg'union
select 9,'La coruña Pepinillos en conserva 250gr',450,29,'~/QuesosFiambres/56.jpg'union
select 9,'El friulano Salamin 1kg',608,62,'~/QuesosFiambres/57.jpg'union
select 9,'Bocatti Salame picado fino 350gr',299,78,'~/QuesosFiambres/58.jpg'union
select 9,'Bocatti Jamon cocido 100gr',120,23,'~/QuesosFiambres/59.jpg'union
select 9,'Ekono Jamon de cerdo 500gr',650,52,'~/QuesosFiambres/60.jpg'union
select 9,'Campo austral Jamon crudo 1kg',1700,125,'~/QuesosFiambres/61.jpg'union
select 9,'Rica Mortadela 250gr',135,78,'~/QuesosFiambres/62.jpg'union
select 9,'Fontana Mortadela de pollo 300gr',257,88,'~/QuesosFiambres/63.jpg'union
select 9,'Éxito Mortadela seleccionada 250gr',300,66,'~/QuesosFiambres/64.jpg'union
select 9,'Blony Salchicha viena especial 150gr',220,8,'~/QuesosFiambres/65.jpg'union
select 9,'Big Dog Salchichas 417gr',340,36,'~/QuesosFiambres/66.jpg'union
select 9,'Kai Salchichas 450gr',298,64,'~/QuesosFiambres/67.jpg'union
select 9,'Zenú Salchichas tradicionales 225gr',241,32,'~/QuesosFiambres/68.jpg'union
select 9,'Castelar Queso azul (Horma entera) 2,5kg',1500,67,'~/QuesosFiambres/69.jpg'union
select 9,'Emperador Queso azul 1/2kg',400,39,'~/QuesosFiambres/70.jpg'union
select 9,'Vacalin Muzzarella 500gr',29,87,'~/QuesosFiambres/71.jpg'union
select 9,'El fortín Queso muzzarella 1kg',445,33,'~/QuesosFiambres/72.jpg'union
select 9,'Clarita Queso cremoso 1kg',325,129,'~/QuesosFiambres/73.jpg'union
select 9,'Castelar Queso cremoso 1kg',359,55,'~/QuesosFiambres/74.jpg'union
select 9,'La paulina Queso cremoso 1kg',405,48,'~/QuesosFiambres/75.jpg'union
select 9,'Montana Queso veldhuyen intenso 1kg',450,69,'~/QuesosFiambres/76.jpg'union
select 9,'Castelar Queso port salut 1kg',420,59,'~/QuesosFiambres/77.jpg'union
select 9,'La paulina Queso crema 250gr',150,28,'~/QuesosFiambres/78.jpg'

update articulos set estado = 1

exec agregarArticulo 7,'Lavandina Querubin 2lt', 123, 100, '~/Imagenes/LavandinaQuerubin.jpg'
exec agregarArticulo 7, 'Jabon Liquido Ariel 3lt', 525, 200, '~/Imagenes/JabonLiquidoAriel.jpg'
exec agregarArticulo 7, 'Desodorante ambiente Glade 420cc', 97, 200, '~/Imagenes/DesodoranteGlade.jpg'
exec agregarArticulo 7, 'Papel higienico scott 4u', 267, 150 , '~/Imagenes/PapelHigienicoScott.jpg'
exec agregarArticulo 7, 'Papel higienico Higienol 4u', 189, 200, '~/Imagenes/PapelHigienico Higienol.jpg'
exec agregarArticulo 7, 'Lavandina en gel Vim 300ml', 62, 250, '~/Imagenes/LavandinaGelVim.jpg'
exec agregarArticulo 7, 'Esponja lisa Virulana', 20, 500, '~/Imagenes/EsponjaVirulana.jpg'
exec agregarArticulo 7, 'Rollitos Virulana 10u', 55, 300, '~/Imagenes/RollitoVirulana.jpg'
exec agregarArticulo 7, 'Escoba Perla 28cm', 240, 350, '~/Imagenes/EscobaPerla.jpg'
exec agregarArticulo 7, 'Mopa Virulana Amarilla', 350, 320, '~/Imagenes/MopaVirulana.jpg'
exec agregarArticulo 7, 'Pala p/ residuos La Perla', 150, 350, '~/Imagenes/PalaResiduos.jpg'
exec agregarArticulo 7, 'Bolsas de residuos 50x70cm', 180, 250, '~/Imagenes/BolsasAsurin.jpg'
exec agregarArticulo 7, 'Repasador tela guarda francesa', 150, 250, '~/Imagenes/RepasadorTelaFrancesa.jpg'
exec agregarArticulo 7, 'Cesto vaiven cuadrado', 275, 250, '~/Imagenes/CestoVaiven.jpg'
exec agregarArticulo 7, 'Escobilla de baño', 90, 250, '~/Imagenes/Escobilla.jpg'
exec agregarArticulo 7, 'Guantes de cocina', 170, 250, '~/Imagenes/.jpg'
exec agregarArticulo 7, 'Canasta liquida Mr.Musculo', 150, 250, '~/Imagenes/.jpg'
exec agregarArticulo 4, 'Cafetera SMARTLIFE 1.5lt', 5609, 100, '~/Imagenes/CafeteraSmartlife.jpg'
exec agregarArticulo 4, 'Horno electrico SMARTLIFE 40lts', 13289, 150, '~/Imagenes/HornoSmartlife.jpg'
exec agregarArticulo 4, 'Heladera Whirlpool 340lts', 75899, 100, '~/Imagenes/HeladeraWhirlpool.jpg'
exec agregarArticulo 4, 'Impresora HP Laser Jet 107a', 14299,200,'~/Imagenes/ImpresoraHP.jpg'
exec agregarArticulo 4, 'Multiprocesadora Liliana 750w', 10999, 150, '~/Imagenes/ProcesadoraLiliana.jpg'
exec agregarArticulo 4, 'Netbook Lenovo corei5 259gb', 115999, 200, '~/Imagenes/NetbookLenovo.jpg'
exec agregarArticulo 4, 'Aire acondicionado Alaska 3500w', 76999, 250, '~/Imagenes/AireAcondicionadoAlaska.jpg'
exec agregarArticulo 4, 'Lavarropas Drean Next 6kg', 57899, 150, '~/Imagenes/LavarropasDrean.jpg'
exec agregarArticulo 4, 'Secarropas Columbia 5500 5.5kg', 8599, 200, '~/Imagenes/SecarropasColumbia.jpg'
exec agregarArticulo 4, 'Microondas BGH 28lts', 28999, 300, '~/Imagenes/MicroondasBGH.jpg'
exec agregarArticulo 4, 'Freezer Sigma 350lts', 48999, 150, '~/Imagenes/FreezerSigma.jpg'
exec agregarArticulo 4, 'Exhibidora Vertical Bambi 370lt', 60000, 100, '~/Imagenes/ExhibidoraBambi.jpg'
exec agregarArticulo 4, 'Heladera bajomesada Vondom 117lts', 50000, 100, '~/Imagenes/HeladeraBajomesada.jpg'
exec agregarArticulo 4, 'Termotanque a gas Rheem', 46999, 100, '~/Imagenes/TermotanqueGas.jpg'
exec agregarArticulo 4, 'Calefon Orbis 14lt', 26000, 100, '~/Imagenes/CalefonOrbis.jpg'
exec agregarArticulo 4, 'Cocina Multigas Peabody', 23999, 100, '~/Imagenes/CocinaPeabody.jpg'
exec agregarArticulo 4, 'Aspiradora ATMA con cable', 10000, 100, '~/Imagenes/AspiradoraAtma.jpg'
exec agregarArticulo 5,pera,10,1500,'~/Imagenes/Pera.jpg'
exec agregarArticulo 5,manzana,15,1000,'~/Imagenes/manzana.jpg'
exec agregarArticulo 5,tomate,20,500,'~/Imagenes/tomate.jpg'
exec agregarArticulo 5,duraznos,50,200,'~/Imagenes/duraznos.jpg'
exec agregarArticulo 5,banana,25,300,'~/Imagenes/banana.jpg'
exec agregarArticulo 1,'galletitas formis',80,250,'~/Imagenes/galletitasformis.jpg'
exec agregarArticulo 1,dannete,40,200,'~/Imagenes/danette.jpg'
exec agregarArticulo 1,malvoro20,200,50,'~/Imagenes/malvoro.jpg'
exec agregarArticulo 1,chupetin,30,100,'~/Imagenes/chupetin.jpg'
exec agregarArticulo 1,encendedor,50,100,'~/Imagenes/encendedor.jpg'

exec agregarArticulo 10,'Falda',250,500,'~/Imagenes/FaldaCarne.jpg'
exec agregarArticulo 10,'Matambre',200,500,'~/Imagenes/Matambre.jpg'
exec agregarArticulo 10,'Costillar',1000,750,'~/Imagenes/Matambre.jpg'
exec agregarArticulo 10,'Bife',180,500,'~/Imagenes/Bife.jpg'
exec agregarArticulo 10,'Peceto',250,500,'~/Imagenes/Peceto.jpg'
exec agregarArticulo 10,'Colita de cuadril',450,800,'~/Imagenes/ColitaDeCuadril.jpg'
exec AgregarArticulo 10,'Tapa de asado',750,1000,'~/Imagenes/TapaDeAsado.jpg'
exec agregarArticulo 6,'Leche',100,1000,'~/Imagenes/Leche.jpg'
exec agregarArticulo 6,'Manteca',150,300,'~/Imagenes/Manteca.jpg'
exec agregarArticulo 6,'Helado',250,800,'~/Imagenes/Helado.jpg'
exec agregarArticulo 6,'Queso',180,600,'~/Imagenes/Queso.jpg'
exec agregarArticulo 6,'Yogurt',90,800,'~/Imagenes/Yogurt.jpg'
exec agregarArticulo 6,'Kefir',250,800,'~/Imagenes/Kefir.jpg'

exec AgregarArticulo 2,'Cerveza Brhama',120,250,'~/Imagenes/brhama.jpg'
exec AgregarArticulo 2,'Cerveza Patagonia',150,250,'~/Imagenes/patagonia.jpg'
exec AgregarArticulo 2,'Vino Termidor',80,250,'~/Imagenes/termidor.jpg'
exec AgregarArticulo 2,'Jaggermeiter',1450,250,'~/Imagenes/jagger.jpg'
exec AgregarArticulo 2,'Vodka Smirnoff',450,250,'~/Imagenes/smirnoff.jpg'

exec AgregarArticulo 3,'McCain papas noisette',140,250,'~/Imagenes/mccainpapasnoisette.jpg'
exec AgregarArticulo 3,'McCain papas fritas',120,250,'~/Imagenes/mccainpapasfritas.jpg'
exec AgregarArticulo 3,'Pizza congelada',243,250,'~/Imagenes/pizzacongelada.jpg'
exec AgregarArticulo 3,'Langostinos Pelados Congelados',346,250,'~/Imagenes/langostinos.jpg'
exec AgregarArticulo 3,'Hamburguesa de soja',163,250,'~/Imagenes/patysoja.jpg'
insert into Articulos(idcategoria,DescripcionArticulo,precioUnitarioArticulo,stockDisponibleArticulo,url_articulo_img)
select '8','Capilatis Shampoo linea proteccion 420ml',345,104,'~/Perfumería/1.jpg' union
select '8','Johnson´s Shampoo baby 200ml',200 ,45 ,'~/Perfumería/2.jpg' union
select '8','Garnier Shampoo Whole Blends 420ml',415 ,32 ,'~/Perfumería/3.jpg' union
select '8','NaturalOE Shampoo nutricion profunda 420ml',320 ,58 ,'~/Perfumería/4.png' union
select '8','SriSri Shampoo ayurvedico 200ml',275 ,14 ,'~/Perfumería/5.jpg' union
select '8','Johnson´s Acondicionador 200ml',270 ,36 ,'~/Perfumería/6.jpg' union
select '8','Veganis Acondicionador nutri 250ml', 270,159 ,'~/Perfumería/7.png' union
select '8','Pantene Acondicionador 200ml',270 ,47,'~/Perfumería/8.jpg' union
select '8','Algobo Acondicionador 930ml',360 ,200 ,'~/Perfumería/9.jpg' union
select '8','Dettol Jabon liquido 220ml',150 ,54 ,'~/Perfumería/10.jpg' union
select '8','SriSri Jabon vegetal 75gr',120 , 69,'~/Perfumería/11.png' union
select '8','Johnson´s Baby jabon 80gr',170 , 88,'~/Perfumería/12.jpg' union
select '8','Algobo Talco 200gr',199 , 13,'~/Perfumería/13.jpg' union
select '8','Rexona Talco efficient 60gr', 215, 51,'~/Perfumería/14.jpg' union
select '8','Ayudin Toallitas desinfectantes',392 ,32 ,'~/Perfumería/15.jpg' union
select '8','Pampers Toallitas humedas 48u',380 ,98 ,'~/Perfumería/16.jpg' union
select '8','Colvert Desodorante 150ml',220 ,123 ,'~/Perfumería/17.jpg' union
select '8','Rexona Barra desodorante Crystal 50gr',200 ,21 ,'~/Perfumería/18.jpg' union
select '8','Axe Desodorante 50gr',180 , 47,'~/Perfumería/19.jpg' union
select '8','Gillette Prestobarba x1',59 , 66,'~/Perfumería/20.jpg' union
select '8','Gillette Venus x2',110 ,39 ,'~/Perfumería/21.jpg' union
select '8','Gillette Prestobarba x3',250 , 57,'~/Perfumería/22.jpg' union
select '8','Gillette Espuma de afeitar 175gr',248 , 87,'~/Perfumería/48.jpg' union
select '8','Dove Crema corporal 400ml',349 ,88 ,'~/Perfumería/23.jpg' union
select '8','Adapt Crema corporal 230ml', 300,49 ,'~/Perfumería/24.png' union
select '8','Natura Crema facial 100cc',330 ,25 ,'~/Perfumería/25.jpg' union
select '8','Adapt Crema de manos 230ml', 390,55 ,'~/Perfumería/26.png' union
select '8','Kdc Crema de manos 50ml',215 ,47 ,'~/Perfumería/27.jpg' union
select '8','Dove Toallitas desmaquillantes',257 , 158,'~/Perfumería/28.jpg' union
select '8','Colagate Enjuague bucal 250ml',222 , 10,'~/Perfumería/29.jpg' union
select '8','Listerine Enjuague bucal 500ml',350 ,45 ,'~/Perfumería/30.jpg' union
select '8','Vitis Cepillo dental x2',172 ,89 ,'~/Perfumería/31.jpg' union
select '8','Oralb Kit cepillos dentales x3',315 , 13,'~/Perfumería/32.jpg' union
select '8','Oralb Hilo dental 25m',235 , 67,'~/Perfumería/33.jpg' union
select '8','Kolynos Pasta dental 105ml',250 ,48 ,'~/Perfumería/34.jpg' union
select '8','Oralb Pasta dental 112ml',303 ,99 ,'~/Perfumería/35.jpg' union
select '8','Biocom Alcohol en gel 220gr',300 ,30 ,'~/Perfumería/36.jpg' union
select '8','Gelicol Alcohol en gel de bolsillo 30gr',120 ,18 ,'~/Perfumería/37.jpg' union
select '8','Bialcohol Alchol etilico 120gr',250 ,102 ,'~/Perfumería/38.jpg' union
select '8','Estrella Algodon super 200gr', 203, 30,'~/Perfumería/39.jpg' union
select '8','Estrella Hisipos 175u',219 ,55 ,'~/Perfumería/40.jpg' union
select '8','QSoft Hisopos 30u',128 , 60,'~/Perfumería/41.jpg' union
select '8','Ayurdeva´s Labial 30gr',450 ,54 ,'~/Perfumería/42.png' union
select '8','Max factor Labial 45gr',750 ,58 ,'~/Perfumería/43.jpeg' union
select '8','Rimmel London Esmalte de uñas 40gr', 290, 200,'~/Perfumería/44.jpeg' union
select '8','Rimmel London Esmalte de uñas 40 gr',300 , 158,'~/Perfumería/45.jpeg' union
select '8','Max factor Rimel 70gr', 661,57 ,'~/Perfumería/46.jpeg' union
select '8','Maybelline Rimel 72gr', 783,21 ,'~/Perfumería/47.jpg'
use TpIntegradorProgramacion
go

create table usuariosXcarrito
(
dni_Usuario varchar(15) not null,
id_articulo int not null,
descripcionArticulo varchar(50) not null,
/*precio decimal(10,1) not null,*/
cantidad int not null,
constraint PK_usuariosXcarrito primary key (dni_usuario, id_articulo),
constraint FK_dni foreign key (dni_usuario)
references usuarios (dniusuario),
constraint FK_articulo foreign key (id_articulo)
references articulos (idarticulo)
)

/* CONSTRUIR DOS PROXIMAS TABLAS POR SEPARADO*/

create procedure agregarArticuloCarrito
@dni int,
@idArticulo int,
@descripcion varchar (50)
as
insert into usuariosXcarrito (dni_Usuario,id_articulo,descripcionArticulo,cantidad)
values (@dni,@idArticulo,@descripcion,1)
go

create procedure modificarCantidadCarrito
@dni int,
@idArticulo int,
@cantidad int
as
update usuariosXcarrito set cantidad = @cantidad where id_articulo=@idArticulo and dni_Usuario = @dni
go

create procedure eliminarArticuloXcarrito
@dni int,
@idArticulo int
as
delete from usuariosXcarrito where id_articulo = @idArticulo and dni_Usuario = @dni
go

create procedure modificarDireccionUsuario
@dni varchar(15),
@direccion varchar(50)
as
update Usuarios set direccionUsuario = @direccion where dniUsuario = @dni
go

create procedure modificarTarjetaUsuario
@dni varchar(15),
@numTarjetaCredito varchar(20),
@Cod varchar(3)
as
update Usuarios set numTarjetaCredito = @numTarjetaCredito, codSeguridad = @Cod where dniUsuario = @dni
go

update Articulos set estado=1
