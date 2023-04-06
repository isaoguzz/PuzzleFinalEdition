<h1>KARE PUZZLE OYUNU </h1>
<br>
<br>
<h4>
Projeyi çalıştırma adımları ;<br>
Projeyi çalıştırmak için yalnızca Visual Studio programı gereklidir. <br>
-> Öncelikle proje içerisinde ki metin belgelerinin yüklenmesi için dilediğiniz bir dosyada project adında bir klasör oluşturunuz. (İsimi değiştirmek istiyorsanız kod içerisinden değiştirebilirisiniz.)<br>
-> Projeyi direkt olarak visual studio ile aç veya rar dosyasını indirerek sln ye aktarınız.<br>
-> Aktardığınız projede bilgisayardan bilgisayara aktarılan exe komutları bulunmaktadır. Bunları bin\debug içerisinden projeyi çalıştırmadan önce silmelisiniz.<br>
-> Daha sonra projeyi çalıştırınız ve oyuna başlayabilirsiniz.<br>

Oyunu oynama adımları;<br>
-> Oyunu açtığımız zaman sağ tarafda bu zamana kadar oynayanların isimleri, hareket sayıları ve skorları tutulmaktadır.<br>
-> Orta tarafda ise puzzle resmini yapıştıracağımız yer bulunmaktadır.<br>
-> Sol tarafda bulunan butonlardan resim seç butonuna tıklayarak yeni resim seçilir.<br>
-> Karıştır butonuna tıklayarak ise resim parçaları en az bir doğru yere gelene kadar karıştırma işlemi yapılmaktadır.<br>
-> Doğru parçayı bir yere yerleştirdiğinizde tekrardan o parça ile oynama yasaklanmıştır.<br>
-> Bütün parçalar doğru yerleştiği taktirde Oyun Bitti mesajı verilmekte ve skorunuz bin klasörü içerisinde yer alan enYuksekSkor.txt belgesine hemde sunucuda ki veritabanına kayıt edilmektedir.<br>

Kullanılan Teknolojiler;<br>
-> Geliştirme C# programlama dili ile visual studio ortamında geliştirilmiştir.<br>
-> Kullanıcı kayıt işlemleri için ADO.NET kullanılmıştır.<br>
-> Sunucu Azure ortamında tutulmuştur, bu sayede veri tabanına erişim her ortamdan sağlanılmıştır.<br>
-> Puzzle parçalarının doğru yer kontrolünün sağlanması için Linkedlist (bağlı liste) veri yapısı kullanılmıştır.<br>
</h4>
<hr>

<h1>SQUARE PUZZLE GAME </h1>
<br>
<br>
<h4>
Steps to run the project;<br>
Only the Visual Studio program is required to run the project.<br>
-> First of all, create a folder named project in a file of your choice for uploading the text documents in the project. (If you want to change the name, you can change it in the code.)<br>
-> Open the project directly with visual studio or download the rar file and transfer it to sln.<br>
-> In the project you transferred, there are exe commands transferred from computer to computer. You should delete them from bin\debug before running the project.<br>
-> Then run the project and you can start the game.<br>

Steps to play the game;<br>
-> When we open the game, the names, number of moves and scores of the players who have played so far are kept on the right side.<br>
-> In the middle, there is a place to paste the puzzle picture.<br>
-> The new picture is selected by clicking the select picture button from the buttons on the left.<br>
-> By clicking on the Mix button, the mixing process is performed until the parts of the picture come to at least one correct place.<br>
-> When you place the correct piece somewhere, it is forbidden to play with that piece again.<br>
-> If all the pieces are placed correctly, the Game Over message is given and your score is recorded in the enYuksekSkor.txt document in the bin folder and in the database on the server.<br>

Used technologies;<br>
-> Development Developed with C# programming language in visual studio environment.<br>
-> ADO.NET is used for user registration.<br>
-> The server is kept in the Azure environment, so access to the database is provided from any environment.<br>
-> Linkedlist (linked list) data structure is used to ensure correct location control of puzzle pieces.<br>
</h4>
