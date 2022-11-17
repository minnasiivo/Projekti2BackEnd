# Projekti2BackEnd

Tämä on padel aiheisen verkkosivuston back-end.
Tämä Asp.Net projekti on kirjoitettu C#-lla.
Tämän rajapinnan avulla pystytään tietoturvallisesti siirtämään käyttäjien rekisteröinti tiedot SQL-tietokantaan.

Tietokannasta pystytään hakemaan padel-pelaajien profiilitietoja, tietoja pelikenttävarauksista sekä käyttämään verkkosivun keskustelupalstaa peliseuran etsimiseen.
Rajapinta vastaa verkkosivulta tuleviin HTTP kyselyihin ja hakee niiden perusteella tietokannsta vaaditut tiedot, tai muokkaa tietokantaa tehdyn pyynnön mukaisesti.

Rajapinnan ominaisuudet ja funkitiot dokumentoidaan swaggerin avulla.

Api toimii Azure alustalla samoin kuin sen käyttämä tietokanta.

Tämän upean koodin ovat luoneet Minna Siivo ja Aurora Särkkä projektityö 2 -kurssilla.
