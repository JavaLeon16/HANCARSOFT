import { RowNode } from 'ag-grid-community';

export interface GridModel {
    refreshData: (page?: number) => void;
    refreshDataKeepSelected: (page?: number) => void;
    selectAll: (value?: boolean) => void;
    getSelectedData: () => any[];
    getSelectedNodes: () => RowNode[];
    showOverlayLoading: () => void;
    showOverlayNoRows: () => void;
    hideOverlay: () => void;
    selectByNodes: (nodes: RowNode[]) => void;
    select: (params: {keys?: string[], key?: string, rows: any[]}) => void;
    changeVisibleColumn: (columnName: string, visible: boolean)=>void;
    ensureIndexVisible: (index: number, position?: string) => void;
    toPage: (page?: number) => void;
}
