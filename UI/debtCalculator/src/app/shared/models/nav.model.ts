import { IconDefinition } from '@fortawesome/free-solid-svg-icons';

export class NavModel {
    name: string;
    route: string;
    icon: IconDefinition;
    administration: boolean;
    collapse?: boolean;
    children?: NavModel[];
}
