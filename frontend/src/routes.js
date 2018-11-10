import Login from '@/components/Login.vue'
import Index from '@/components/Index.vue'
import PageList from '@/components/PageList.vue'
import PageOne from '@/components/PageOne.vue'

export default [{
        path: '/login',
        name: 'Login',
        component: Login
    },
    {
        path: '/index',
        name: 'Index',
        component: Index
    },
    {
        path: '/pagelist/:mccaption',
        name: 'PageList',
        component: PageList
    },
    {
        path: '/pageone/:mccaption&:record',
        name: 'PageOne',
        component: PageOne
    }
]