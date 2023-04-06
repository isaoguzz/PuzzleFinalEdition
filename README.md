<h1>KARE PUZZLE OYUNU </h1>
<br>
<br>
<h4>
Projeyi çalıştırma adımları ;
Projeyi çalıştırmak için yalnızca Visual Studio programı gereklidir.
-> Öncelikle proje içerisinde ki metin belgelerinin yüklenmesi için dilediğiniz bir dosyada project adında bir klasör oluşturunuz. (İsimi değiştirmek istiyorsanız kod içerisinden değiştirebilirisiniz.)
-> Projeyi direkt olarak visual studio ile aç veya rar dosyasını indirerek sln ye aktarınız.
-> Aktardığınız projede bilgisayardan bilgisayara aktarılan exe komutları bulunmaktadır. Bunları bin\debug içerisinden projeyi çalıştırmadan önce silmelisiniz.
-> Daha sonra projeyi çalıştırınız ve oyuna başlayabilirsiniz.

Oyunu oynama adımları;
-> Oyunu açtığımız zaman sağ tarafda bu zamana kadar oynayanların isimleri, hareket sayıları ve skorları tutulmaktadır.
-> Orta tarafda ise puzzle resmini yapıştıracağımız yer bulunmaktadır.
-> Sol tarafda bulunan butonlardan resim seç butonuna tıklayarak yeni resim seçilir.
-> Karıştır butonuna tıklayarak ise resim parçaları en az bir doğru yere gelene kadar karıştırma işlemi yapılmaktadır.
-> Doğru parçayı bir yere yerleştirdiğinizde tekrardan o parça ile oynama yasaklanmıştır.
-> Bütün parçalar doğru yerleştiği taktirde Oyun Bitti mesajı verilmekte ve skorunuz bin klasörü içerisinde yer alan enYuksekSkor.txt belgesine hemde sunucuda ki veritabanına kayıt edilmektedir.

Kullanılan Teknolojiler;
-> Geliştirme C# programlama dili ile visual studio ortamında geliştirilmiştir.
-> Kullanıcı kayıt işlemleri için ADO.NET kullanılmıştır.
-> Sunucu Azure ortamında tutulmuştur, bu sayede veri tabanına erişim her ortamdan sağlanılmıştır.
-> Puzzle parçalarının doğru yer kontrolünün sağlanması için Linkedlist (bağlı liste) veri yapısı kullanılmıştır.
</h4>
<hr>

<h1>SQUARE PUZZLE GAME </h1>
<br>
<br>
<h4>
Steps to run the project;
Only the Visual Studio program is required to run the project.
-> First of all, create a folder named project in a file of your choice for uploading the text documents in the project. (If you want to change the name, you can change it in the code.)
-> Open the project directly with visual studio or download the rar file and transfer it to sln.
-> In the project you transferred, there are exe commands transferred from computer to computer. You should delete them from bin\debug before running the project.
-> Then run the project and you can start the game.

Steps to play the game;
-> When we open the game, the names, number of moves and scores of the players who have played so far are kept on the right side.
-> In the middle, there is a place to paste the puzzle picture.
-> The new picture is selected by clicking the select picture button from the buttons on the left.
-> By clicking on the Mix button, the mixing process is performed until the parts of the picture come to at least one correct place.
-> When you place the correct piece somewhere, it is forbidden to play with that piece again.
-> If all the pieces are placed correctly, the Game Over message is given and your score is recorded in the enYuksekSkor.txt document in the bin folder and in the database on the server.

Used technologies;
-> Development Developed with C# programming language in visual studio environment.
-> ADO.NET is used for user registration.
-> The server is kept in the Azure environment, so access to the database is provided from any environment.
-> Linkedlist (linked list) data structure is used to ensure correct location control of puzzle pieces.
</h4>
