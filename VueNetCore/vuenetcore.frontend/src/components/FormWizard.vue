<template>
    <div>
        <ul class="steps has-content-centered">
            <li class="steps-segment" v-for="tab in tabs" :class="{ 'is-active': tab.isActive }" v-bind:key="tab.name">
                <span class="steps-marker"></span>
                <div class="steps-content">
                    <p class="is-size-4">{{tab.name}}</p>
                    <p>{{tab.info}}</p>
                </div>
            </li>
        </ul>                    
        <div class="tab-details">
            <slot :formData="formData"></slot>
        </div>
        <br/>
        <div class="field is-grouped">
            <div class="control" v-if="currentActive > 0">
                <button @click="previousTab()" class="button is-primary">Previous</button>
            </div>
            <div class="control" v-if="currentActive < totalTabs - 1">
                <button @click="nextTab()" class="button is-link">Next</button>
            </div>
            <div class="control" v-if="currentActive == totalTabs -1" >
                <button @click="submit()" class="button is-success">Submit</button>
            </div>
        </div>
                
    </div>
</template>


<script>
import axios from 'axios';

export default {
  name: 'form-wizard',
  
  data(){
    return{
        tabs: [],
        currentActive: 0,
        totalTabs: 0,
        formData:{
          name: '',
          email: '',
          about: '',
          gender: '',
          reference: '',
          terms: '',     
     },
     API_URL: process.env.VUE_APP_API_URL        
    }
   },

    created(){
        this.tabs = this.$children;
        
    },

    mounted(){
        this.totalTabs = this.tabs.length;
    },

    methods:{

        previousTab(){
            this.currentActive--;
            this.tabs.forEach(tab => {
                tab.isActive = false;
            });
                this.tabs[this.currentActive].isActive = true;

        },

        nextTab(){
            this.$root.$validator.validate('step'+(this.currentActive+ 1)+'.*').then(valid => {
                if (valid){
                    this.currentActive++;
                    this.tabs.forEach(tab => {
                        tab.isActive = false;
                    });
            
                    this.tabs[this.currentActive].isActive = true;
                }
            }); 

        },

        submit(){            
            this.$root.$validator.validate('step'+this.totalTabs+'.*').then(valid => {
                if (valid){
                    var data = JSON.stringify(this.formData)              
                    axios.post(`${this.API_URL}/form/`, data, {
                        headers: { 'Content-Type': 'application/json' }
                    });
                }
            });
        }  
    }
}
</script>