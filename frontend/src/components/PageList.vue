<template>
<div class="row">
    <TopBar></TopBar>
    <div class="row">
        <b-table striped hover :items="myProvider" :fields="fields">
            <template slot="show_details" slot-scope="row">
                <b-button size="sm" @click.stop="row.toggleDetails" class="mr-2">
                    {{ row.detailsShowing ? 'Hide' : 'Show'}} Details
                </b-button>
            </template>
        </b-table>
    </div>
</div>
</template>

<script>
import Axios from "axios";
import TopBar from "@/components/TopBar.vue";

import metadata from "@/metadata";
Axios.defaults.withCredentials = true;

export default {
  components: {
    TopBar
  },
  methods: {
    myProvider(ctx) {
      let promise = Axios.get(
        `http://localhost/tyreacpasms/DefaultHandler.ashx?method=get${
          this.$route.params.mccaption
        }list`
      );
      return promise
        .then(data => {
          return data.data.rows;
        })
        .catch(error => {
          return [];
        });
    }
  },
  data() {
    var fields = [
      ...metadata[this.$route.params.mccaption].Columns.map(col => ({
        key: col.column_name,
        label: col.column_description,
        sortable: true
      }))
    ];
    return {
      fields,
      metadata: metadata[this.$route.params.mccaption]
    };
  }
};
</script>
