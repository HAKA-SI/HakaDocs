import { gsap } from 'gsap';

// <div id="monElement1">Contenu de votre composant</div>
// import { fadeInAnimation, slideInAnimation } from 'chemin/vers/animations';
// const element1 = document.getElementById('element1');
// fadeInAnimation(element1);

export const fadeInAnimation = (element: HTMLElement) => {
  gsap.from(element, {
    opacity: 0,
    duration: 1,
    ease: 'power2.out'
  });
};

export const slideInAnimation = (element: HTMLElement) => {
  gsap.from(element, {
    x: -100,
    duration: 1,
    ease: 'power2.out'
  });
};

// Animation de la page complète (entrée ou sortie) 
export const animatePageEntry = () => {
    gsap.from('body', {
      opacity: 0,
      duration: 1,
      ease: 'power2.out'
    });
  };
  
  export const animatePageExit = () => {
    gsap.to('body', {
      opacity: 0,
      duration: 1,
      ease: 'power2.out'
    });
  };


  // Animation d'une carte (entrée ou sortie) :
  export const animateCardEntry = (cardElement: HTMLElement) => {
    gsap.from(cardElement, {
      opacity: 0,
      y: -50,
      duration: 1,
      ease: 'power2.out'
    });
  };
  
  export const animateCardExit = (cardElement: HTMLElement) => {
    gsap.to(cardElement, {
      opacity: 0,
      y: 50,
      duration: 1,
      ease: 'power2.out'
    });
  };

//   Animation d'une image (zoom, rotation, etc.) :
  export const animateImageZoom = (imageElement: HTMLElement) => {
    gsap.from(imageElement, {
      scale: 0,
      duration: 1,
      ease: 'power2.out'
    });
  };
  
  export const animateImageRotation = (imageElement: HTMLElement) => {
    gsap.to(imageElement, {
      rotation: 360,
      duration: 1,
      ease: 'power2.out'
    });
  };

//Animation d'un effet au clic
  export const animateButtonClick = (buttonElement: HTMLElement) => {
    buttonElement.addEventListener('click', () => {
      gsap.fromTo(buttonElement, {
        scale: 1,
        opacity: 1
      }, {
        scale: 0.8,
        opacity: 0.5,
        duration: 0.3,
        ease: 'power2.out',
        yoyo: true
      });
    });
  };
  
//Animation de chargement de page
  export const animatePageLoad = (loaderElement: HTMLElement, onComplete: () => void) => {
    gsap.to(loaderElement, {
      opacity: 0,
      duration: 1,
      ease: 'power2.out',
      onComplete
    });
  };
  
  
  