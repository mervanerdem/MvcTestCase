# MvcTestCase

## 1.	Ürün Girişi Sayfasının Oluşturulması

Veri tabanındaki Product tablosunu kullanarak yeni ürün girişi ve varolan ürünlerin güncellenmesi işleminin yapılabildiği sayfanın oluşturulması.

## 2.	 Satış Ekranının Oluşturulması
Veri tabanındaki Sales tablosu satışların tutulduğu tabloya karşılık gelmektedir. Bu tabloda ürünün liste fiyatı, satış fiyatı, kaç adet satıldığı ve kime satıldığı gibi verilerin tutulduğu alanlar mevcuttur. Ürünün liste fiyatı ve satış fiyatı üzerinden satıcının ne kadar indirim yaptığı hesaplanarak DISCOUNTRATE sütunu doldurulmalıdır. 
Sales tablosunda ürünün liste fiyatı (LISTPRICE) ve satış fiyatı (SALESPRICE) alanları var. Bu alanlardan LISTPRICE olanı ürün seçildiğinde, ürün tablosundaki (SALPRICE) alanından dolacak.
SALESPRICE alanı ise kullanıcı tarafından değiştirilebilir olacak ve satış fiyatı değiştirdikçe liste fiyatına oranla satıcının ne kadar indirim yapıldığı hesaplanarak iskonto oranı bulunacak ve bu oran DISCOUNTRATE alanına yazılacak. 
Sistemde her ürünün depo stoğu kayıtlı olduğundan satış esnasında yapılan satışa ait verinin Stock tablosuna yazılması gerekmektedir. Ayrıca Satış sırasında ürünün depoda eksiye düşmemesi adına kontrol yapılmalıdır.
Kullanıcı satış ekranında, satışın kime yapıldığını karşısına çıkan listeden (Müşteri adını ve numarasını birlikte) görmeli ve seçmelidir.

## 3.	Satın Alım Ekranının Oluşturulması
Veri tabanındaki Purchase tablosu satın alma kayıtlarının tutulduğu tabloya karşılık gelmektedir. Bu tabloda ürünün alım fiyatı, kaç adet alındığı ve kimden alındığı gibi verilerin tutulduğu alanlar mevcuttur.
Satışta olduğu gibi alımda da yapılan alıma ait verinin Stock tablosuna yazılması gerekmektedir. 



## 4.	Listeleme Ekranlarının Oluşturulması
-	Stok Raporu: Ürünlerin Toplam Stok Durumunu gösteren bir rapor. 
