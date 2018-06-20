import { NgModule } from '@angular/core';
import { IconHome, IconShoppingCart, IconLayers, IconPlus, IconWatch, IconCamera, IconGithub, IconRefreshCw } from 'angular-feather';

const icons = [
  IconHome,
  IconShoppingCart,
  IconLayers,
  IconPlus,
  IconWatch,
  IconCamera,
  IconGithub,
  IconRefreshCw
];

@NgModule({
  exports: icons
})
export class IconsModule { }
