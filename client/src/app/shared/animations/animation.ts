import { trigger, state, style, transition, animate } from '@angular/animations';

export const fadeInAnimation = trigger('fadeIn', [
  state('void', style({ opacity: 0 })),
  state('*', style({ opacity: 1 })),
  transition('void => *', animate('500ms ease-in')),
]);

export const slideInAnimation = trigger('slideIn', [
  state('void', style({ transform: 'translateX(-100%)' })),
  state('*', style({ transform: 'translateX(0)' })),
  transition('void => *', animate('500ms ease-in')),
]);

export const bounceInAnimation = trigger('bounceIn', [
    state('void', style({ transform: 'scale(0.2)' })),
    state('*', style({ transform: 'scale(1)' })),
    transition('void => *', animate('500ms ease-in')),
    transition('* => void', animate('500ms ease-out'))
  ]);

  
  export const flipInAnimation = trigger('flipIn', [
    state('void', style({ transform: 'rotateY(180deg)' })),
    state('*', style({ transform: 'rotateY(0)' })),
    transition('void => *', animate('500ms ease-in')),
    transition('* => void', animate('500ms ease-out'))
  ]);

  export const scaleInAnimation = trigger('scaleIn', [
    state('void', style({ transform: 'scale(0)' })),
    state('*', style({ transform: 'scale(1)' })),
    transition('void => *', animate('500ms ease-in')),
    transition('* => void', animate('500ms ease-out'))
  ]);

  

// import { fadeInAnimation, slideInAnimation } from 'chemin/vers/animations'; dans le component
// animations: [fadeInAnimation, slideInAnimation], dans le components
// <div [@fadeIn]>Contenu de votre composant</div> dans le html
// <div [@slideIn]>Autre contenu du composant</div> dans le html

