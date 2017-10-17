# BeerInventoryApp

A Xamarin Forms Application that targets Mobile, Tablet, and Desktop with one codebase and miminum unshared code.

Consumes the BeerInventoryAPI as a source of information and management.

Offers per-user access to scan Beer into / out of different locations of stock to help user keep track of what beer they own, and which of the beer is cold.

App has a companion script that runs on a Raspberry Pi to utilize a wireless barcode scanner to simplify scanning process.

## Roadmap

- [X] Basic UI
- [X] UPC Scanning via ZXing
- [X] User SSO Authentication via Auth0
- [X] Android Implementation
- [X] UWP Implemenation
- [X] Azure Search as a search solution
- [ ] UI Rework after MVP is done
- [ ] iOS Implementation
- [ ] Swap to .Net Core 2.0 as core library format
- [ ] Decide on using XAML or Code for UI layouts across the board
