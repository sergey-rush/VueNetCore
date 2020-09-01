<template>

  <div class="main-content">

    <div class="main-content__top">
        <h1 class="main-content__title">
            Search
        </h1>
    </div>

    <div class="main-content__body">
<div class="content">
      <p>
        Please enter text to search form data and press "Enter"
      </p>
</div>
 <form @submit.prevent="submit">
<div class="field has-addons">
  <div class="control is-expanded">
    <input class="input" name="searchText" type="text" v-model="query" placeholder="Enter text to search">
  </div>
  <div class="control">
    <a class="button is-info" v-on:click="submit">
      Search
    </a>
  </div>
</div>
 </form>

      <table class="table">      
      <tr v-for="result in results" :key="result.id">
        <td>{{ result.payload }}</td>
      </tr>
    </table>
  </div>
  </div>
</template>


<script>
import axios from 'axios';
export default {  
  name: 'search',
  
  data(){
    return {
      query:'',
      results:null,      
      API_URL: process.env.VUE_APP_API_URL
    }
  },
  computed: {
		todos () {
			return this.results.length;
		}
  }, 
  methods: {
    submit(){      
      this.results = null;
      axios.get(`${this.API_URL}/search?query=${this.query}`, {
        headers: {
          'Access-Control-Allow-Origin': true,
        }
        }).then(response => (this.results = response.data))
  .catch(function (error) {    
    alert("Error: ", error);
  });
      }      
  }
}

</script>
