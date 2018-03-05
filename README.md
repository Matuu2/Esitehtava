# Esitehtava
Eatechin esitehtava:
Toiminnallisuudet:

  Auton lisäys
      api/car/add/?ominaisuus
      -Lisätään auto tietokantaan antamalla ominaisuudet query stringinä.
        Mikäli jotain ominaisuutta ei anna niin sen arvoksi tulee null
        
  Auton muokkaus
      -api/car/update/{id}/?ominaisuus
      -Annetaan ominaisuuksissa uusien ominaisuuksien arvot
      
  Auton poistaminen
      -api/delete/{id}
      -poistaa auton, jolla on kyseinen id mikäli se löytyy tietokannasta
      
  Auton tietojen hakeminen
      -api/car/{id}
      -poistaa auton tiedot tietokannasta, mikäli id löytyy tietokannasta
      
  Autolistaus ilman rajauksia
      -api/car/
      -Listaa kaikki autot
      
  Autolistaus rajauksilla
      -api/car/list/?min?max?brand/model
      -listaa autot, jotka täyttävät annetut kriteerit. Jos kriteerejä ei anneta niin listaa kaikki autot.
      
