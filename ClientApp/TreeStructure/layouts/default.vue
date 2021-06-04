<template>
  <v-app dark>
    <Header></Header>
    <v-main>
      <v-container fluid>
        <nuxt />
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
import Header from '../../TreeStructure/components/header/Header.vue'
export default {
  components: { Header },
}
</script>
