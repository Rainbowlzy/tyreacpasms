<template>
<div class="row">
    <b-table striped hover :items="myProvider"  :fields="fields">

      
    <template slot="show_details" slot-scope="row">
            <b-button size="sm" @click.stop="row.toggleDetails" class="mr-2">
       {{ row.detailsShowing ? 'Hide' : 'Show'}} Details
      </b-button>
    </template>
    </b-table>
</div>
</template>

<script>
import Axios from "axios";

import metadata from "@/metadata";
Axios.defaults.withCredentials = true;
const items = [
  {
    isActive: true,
    age: 40,
    first_name: "Dickerson",
    last_name: "Macdonald"
  },
  {
    isActive: false,
    age: 21,
    first_name: "Larsen",
    last_name: "Shaw"
  },
  {
    isActive: false,
    age: 89,
    first_name: "Geneva",
    last_name: "Wilson"
  },
  {
    isActive: true,
    age: 38,
    first_name: "Jami",
    last_name: "Carney"
  }
];
// k = {
//   debug: null,
//   Columns: [
//     {
//       column_name: "MCCaption",
//       column_description: "标题",
//       dbtype: "NVARCHAR(4000)",
//       max_length: 0,
//       pascal_column_name: "Caption"
//     }
//   ],
//   Children: null,
//   Parent: null,
//   r: null,
//   table_name: "MenuConfiguration",
//   table_name_en: "MenuConfiguration",
//   table_name_ch: "菜单配置",
//   column_name: null,
//   column_description: null,
//   dbtype: null,
//   max_length: 0
// };

export default {
  methods: {
    myProvider(ctx) {
      // Here we don't set isBusy prop, so busy state will be handled by table itself
      // this.isBusy = true
      let promise = Axios.get(
        `http://localhost/tyreacpasms/DefaultHandler.ashx?method=get${
          this.$route.params.mccaption
        }list`
      );

      return promise
        .then(data => {
          const items = data.items;
          // Here we could override the busy state, setting isBusy to false
          // this.isBusy = false
          return items;
        })
        .catch(error => {
          // Here we could override the busy state, setting isBusy to false
          // this.isBusy = false
          // Returning an empty array, allows table to correctly handle busy state in case of error
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
      items,
      fields,
      metadata: metadata[this.$route.params.mccaption]
    };
  }
};
</script>
