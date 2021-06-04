
<template>
  <ul class="flex flex-col lg:flex-row gap-10">
    <v-col >
      <v-treeview
        style="min-width: 199px"
        :active.sync="active"
        activatable
        :items="items"
        item-children="subCategories"
        return-object
      ></v-treeview>
    </v-col>
  </ul>
</template>

<script>
export default {
  data() {
    return {
      items: [],
    };
  },
  async fetch() {
    let items = await this.$axios.$get(
      "/api/Categories/Tree"
    );
    this.items=items
  },
};
</script> 
