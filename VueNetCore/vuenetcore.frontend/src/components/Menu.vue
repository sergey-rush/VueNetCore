<template>

  <div class="menu-container">

    <!-- root level itens -->
    <ul class="menu">

      <li class="menu__top">
        <router-link to="/" class="menu__logo">
          <img src="/icon-32.png" alt="icon">
        </router-link>
        <a
        href="#"
        @click.prevent="openProjectLink"
        class="menu__title"
        >
          Task App
        </a>
      </li>

      <li>
        <a
        href="#"
        @click.prevent="updateMenu('home')"
        :class="highlightSection('home')"
        >
            <i class="fa fa-home menu__icon" aria-hidden="true"></i>
            Home
        </a>
      </li>

      <li>
        <a
        href="#"
        @click.prevent="updateMenu('search')"
        :class="highlightSection('search')"
        >
            <i class="fa fa-search menu__icon" aria-hidden="true"></i>
            Search
        </a>
      </li> 
    </ul>

 

  </div>

</template>

<script>
import menuData from './support/menu-data';
import kebabCase from 'lodash/kebabCase';

export default {
  name: 'Menu',
  data(){
    return {
      contextSection: '',
      menuItens: [],
      menuData: menuData,
      activeSubMenu: '',
      prevPath:'home',
    }
  },

  methods: {

    openProjectLink() {
      alert('You could open the project frontend in another tab here, so the logged admin could see changes made to the project ;)');
    },

    updateMenu(context) {
      this.contextSection = context;
      this.menuItens = this.menuData[context];      
      if (!this.prevPath){
        this.prevPath = context;
        return;
      }
      
      if (this.prevPath !== context) {        
        if (context === 'home') {
        this.$router.push('/');
      }

      if (context === 'search') {
        this.$router.push('/search');
      }
      
      this.prevPath = context;
      }      
    },

    highlightSection(section) {
      return {
        'menu__link': true,
        'menu__link--active': section === this.contextSection,
      };
    },

    subMenuClass(subMenuName) {
      return {
        'context-menu__link': true,
        'context-menu__link--active': this.activeSubMenu === subMenuName,
      };
    },

    closeContextMenu() {
      this.contextSection = '';
      this.menuItens = [];
    },

    openSection(item) {
      this.activeSubMenu = item.txt;

      this.$router.push(this.getUrl(item));
      window.bus.$emit('menu/closeMobileMenu');
    },

    getUrl(item) {
      let sectionSlug = kebabCase(item.txt);

      return `${item.link}/${sectionSlug}`;
    }

  }

}
</script>
