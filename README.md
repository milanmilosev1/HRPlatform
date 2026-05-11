# HRPlatform

Coding task za .NET praksu u kompaniji Intens. 
Platforma za dodavanje i nadgledanje klijenata i njihovih vestina.

## Arhitektura

Za projekat sam koristio malo pojednostavljenu Clean arhitekturu. Smatram da je ona najbolja za ovaj tip projekta koji nije previse kompleksan, jer omogucava jasnu separaciju odgovornosti izmedju slojeva i olaksava buduce prosirenje sistema.


| Sloj |  Namena  | 
|:-----|:--------:|
| Domain | Definisanje entiteta, DTO objekata, kao i interfejsa za servise i repozitorijume |
| Data   | Pristup bazi podataka (DbContext, migracije i implementacije repozitorijuma) |
| Service   | Implementacija biznis logike, validacija i pomocnih klasa   |
| WebApi   | Definisanje API endpoint-a i kontrolera |
| Tests | Unit testovi za biznis logiku |

## Tech Stack

**Client:** Trenutno ne postoji, ali kada bih razvijao klijentski deo verovatno bih koristio React + Typescript

**Server:** .NET 8 (dokumentovani endpoint-i koristeci Swagger)

**Baza podataka:** PostgreSQL (hostovan na Supabase platformi)

**ORM:** Entity Framework Core

## Izazovi

Najveci izazov tokom razvoja bio je implementacija Result pattern-a za obradu gresaka i propagaciju rezultata kroz aplikaciju, s obzirom na to da se prvi put detaljnije susrecem sa ovim pristupom.

## Sta sledece?

Sledeci korak bio bi razvoj klijentske aplikacije.

Takodje, jedna od ideja za buduce prosirenje sistema jeste podrska za vise kompanija i HR korisnika, cime bi aplikacija presla iz internog alata u multi-tenant sistem. To bi podrazumevalo implementaciju korisnickih naloga, autentikacije, autorizacije i dodatnih bezbednosnih mehanizama.
