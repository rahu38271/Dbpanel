import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'home',
    loadChildren: () => import('./home/home.module').then( m => m.HomePageModule)
  },
  {
    path: 'data',
    loadChildren: () => import('./data-module/data-module.module').then( m => m.DataModuleModule)
  },
  {
    path: '',
    loadChildren: () => import('./login-module/login-module.module').then( m => m.LoginModuleModule)
  },
  {
    path: 'otp',
    loadChildren: () => import('./otp-module/otp-module.module').then( m => m.OtpModuleModule)
  },
  {
    path: 'list',
    loadChildren: () => import('./list-module/list-module.module').then( m => m.ListModuleModule)
  },
  {
    path: 'user',
    loadChildren: () => import('./user-module/user-module.module').then( m => m.UserModuleModule)
  },
  {
    path: 'bulkUpload',
    loadChildren: () => import('./bulk-module/bulk-module.module').then( m => m.BulkModuleModule)
  },
  {
    path: 'permission',
    loadChildren: () => import('./permission-module/permission-module.module').then( m => m.PermissionModuleModule)
  },
  {
    path: 'setting',
    loadChildren: () => import('./setting-module/setting-module.module').then( m => m.SettingModuleModule)
  },
  {
    path: 'role',
    loadChildren: () => import('./role-module/role-module.module').then( m => m.RoleModuleModule)
  },
  {
    path: 'history',
    loadChildren: () => import('./history-module/history-module.module').then( m => m.HistoryModuleModule)
  },
  {
    path: 'search',
    loadChildren: () => import('./search-module/search-module.module').then( m => m.SearchModuleModule)
  },
  {
    path: 'name',
    loadChildren: () => import('./name-module/name-module.module').then( m => m.NameModuleModule)
  },
  {
    path: 'multipin',
    loadChildren: () => import('./multipin-module/multipin-module.module').then( m => m.MultipinModuleModule)
  },
  
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
